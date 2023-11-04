using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Movement
{

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private float _smoothInputSpeed = 1;

        [Header("Ground Movement")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;

        [Header("Dash movement")]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private AnimationCurve _slideSpeedCurve;

        private IInputService _inputService;
        private CharacterController _characterController;

        private Vector3 _velocity;
        private Vector3 _dashDirection;
        private Vector2 _movement;
        private Vector2 _smoothInputVelocity;

        private float _dashTime;

        private bool _isDash = false;
        private bool _isRun = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void InitInput(IInputService inputService)
        {
            _inputService = inputService;

            inputService.OnDashPress += () =>
            {
                if (_isDash)
                    return;

                _dashTime = 0;

                _dashDirection = _movement;
                _isDash = true;
            };

            inputService.OnRunPress += () => _isRun = true;
            inputService.OnRunErased += () => _isRun = false;
        }


        private void Update()
        {
            if (_isDash)
                DashMovement();
            else
                GroundMovement();
        }

        private void DashMovement()
        {
            Vector3 tempVelocity;
            _dashTime += Time.deltaTime;

            if (_dashDuration > _dashTime)
            {
                tempVelocity = _dashDirection * _dashSpeed * _slideSpeedCurve.Evaluate(_dashTime);

                _velocity.x = tempVelocity.x;
                _velocity.z = tempVelocity.y;

                _characterController.Move(_velocity * Time.deltaTime);
                return;
            }

            _isDash = false;
        }

        private void GroundMovement()
        {
            Vector2 input = _inputService.MoveValue;
            _movement = Vector2.SmoothDamp(_movement, input, ref _smoothInputVelocity, 1 / _smoothInputSpeed);

            Vector3 tempVelocity;
            if (_isRun)
                tempVelocity = new Vector3(_movement.x * _runSpeed, 0, _movement.y * _runSpeed);
            else
                tempVelocity = new Vector3(_movement.x * _walkSpeed, 0, _movement.y * _walkSpeed);

            _velocity.x = tempVelocity.x;
            _velocity.z = tempVelocity.z;

            _characterController.Move(_velocity * Time.deltaTime);
        }
    }

}