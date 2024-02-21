using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Management.Input;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private Animator _animator;

        [Header("Input Settings")]
        [SerializeField] private float _smoothInputSpeed = 1;
        [SerializeField] private float _castWidth;
        [SerializeField] private LayerMask groundMask;

        [Header("Ground Movement")]
        [SerializeField] private float _runSpeedCoeff;

        [Header("Dash movement")]
        [SerializeField] private float _dashSpeedCoeff;
        [SerializeField] private float _dashDuration;
        [SerializeField] private float _dashCooldown;
        [SerializeField] private AnimationCurve _slideSpeedCurve;

        private DashController _dashController;
        private IValueInput _inputService;
        private CharacterController _characterController;

        private Vector3 _velocity = new Vector3(0, 0, 0);
        private Vector3 _dashDirection;
        private Vector2 _movement;
        private Vector2 _smoothInputVelocity;

        private float _dashTime;
        private float _speed;

        private bool _isGrounded;
        private bool _isDash = false;
        private bool _isRun = false;

        public event Action OnDashEnd;

        private void Awake()
        {
            _dashController = new DashController(_dashCooldown);
            _characterController = GetComponent<CharacterController>();

            _playerEntity.CharacterDataWrapper.OnStatChanged += CalculateSpeed;
        }

        private void Gravity()
        {
            if (!_isGrounded)
                _velocity.y = -9;
            else
                _velocity.y = 0;
        }

        private void Start()
        {
            CalculateSpeed(StatTypesEnum.Speed);
        }

        private void CheckGround()
        {
            if (Physics.CheckSphere(transform.position, _castWidth, groundMask.value))
            {
                _isGrounded = true;
                return;
            }

            _isGrounded = false;
        }

        private void CalculateSpeed(StatTypesEnum targetValue)
        {
            if (targetValue == StatTypesEnum.Speed || targetValue == StatTypesEnum.All)
                _speed = _playerEntity.CharacterDataWrapper.GetValue(StatTypesEnum.Speed);
        }

        public void InitInput(IValueInput inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            CheckGround();
            Gravity();

            if (_isDash)
                DashMovement();
            else
                GroundMovement();
        }

        public void SetRun(bool value)
        {
            _isRun = value;
        }

        public void SetDash(bool value)
        {
            if (value && !_dashController.IsAbleToDash())
            {
                OnDashEnd?.Invoke();
                return;
            }

            _dashDirection = _movement.normalized;
            _dashTime = 0;

            _isDash = value;
        }

        private void DashMovement()
        {
            Vector3 tempVelocity;
            _dashTime += Time.deltaTime;

            if (_dashDuration > _dashTime)
            {
                tempVelocity = _dashDirection * _speed * _dashSpeedCoeff * _slideSpeedCurve.Evaluate(_dashTime);

                _velocity.x = tempVelocity.x;
                _velocity.z = tempVelocity.y;

                _characterController.Move(_velocity * Time.deltaTime);
                return;
            }

            _dashController.DashEnded();
            OnDashEnd?.Invoke();
            _isDash = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, _castWidth);
        }

        private void GroundMovement()
        {
            Vector2 input = _inputService.MoveValue;
            _movement = Vector2.SmoothDamp(_movement, input, ref _smoothInputVelocity, 1 / _smoothInputSpeed);

            if (input.x != 0 || input.y != 0)
                _animator.SetFloat("Walk", 1);
            else
                _animator.SetFloat("Walk", -1);

            float curSpeed = _speed;
            if (_isRun)
                curSpeed *= _runSpeedCoeff;

            Vector3 tempVelocity = new Vector3(_movement.x * curSpeed, 0, _movement.y * curSpeed);

            _velocity.x = tempVelocity.x;
            _velocity.z = tempVelocity.z;

            _characterController.Move(_velocity * Time.deltaTime);
        }
    }

}