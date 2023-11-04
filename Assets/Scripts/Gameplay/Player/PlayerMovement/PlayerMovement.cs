using HalloGames.RavensRain.Management.Input;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;

        [Header("Input Settings")]
        [SerializeField] private float _smoothInputSpeed = 1;

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

        private Vector3 _velocity;
        private Vector3 _dashDirection;
        private Vector2 _movement;
        private Vector2 _smoothInputVelocity;

        private float _dashTime;
        private float _speed;

        private bool _isDash = false;
        private bool _isRun = false;

        public event Action OnDashEnd;

        private void Awake()
        {
            _dashController = new DashController(_dashCooldown);
            _characterController = GetComponent<CharacterController>();

            _playerEntity.CharacterDataWrapper.OnStatChanged += (stat) => CalculateSpeed();

            CalculateSpeed();
        }

        private void CalculateSpeed()
        {
            _speed = _playerEntity.CharacterDataWrapper.GetValue(Characters.Stats.StatTypesEnum.Speed);
        }

        public void InitInput(IValueInput inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
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

        private void GroundMovement()
        {
            Vector2 input = _inputService.MoveValue;
            _movement = Vector2.SmoothDamp(_movement, input, ref _smoothInputVelocity, 1 / _smoothInputSpeed);


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