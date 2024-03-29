using HalloGames.Architecture.Services;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Input
{
    public class InputController : MonoBehaviour, IActionInput, IValueInput
    {
        [SerializeField] private BackgroundRaycaster _backgroundRaycaster;

        private CustomInput _inputActions;

        public event Action OnLeftFirePress;
        public event Action OnLeftFireErase;
        public event Action OnRightFirePress;
        public event Action OnDashPress;
        public event Action OnRunErased;
        public event Action OnRunPress;
        public event Action OnUtilityPress;
        public event Action OnPickUpPress;

        public Vector2 MoveValue
        {
            get
            {
                return _inputActions.Player.Move.ReadValue<Vector2>();
            }
        }

        public Vector3 PlayerMouseWorldPos
        {
            get
            {
                return _backgroundRaycaster.GetWorldMousePos();
            }
        }

        private void Awake()
        {
            _inputActions = new CustomInput();
            InitInput();
        }

        private void OnDestroy()
        {
            ResetInput();
        }

        private void InitInput()
        {
            _inputActions.Player.LeftFire.performed += (ctx) => OnLeftFirePress?.Invoke();
            _inputActions.Player.LeftFire.canceled += (ctx) => OnLeftFireErase?.Invoke();
            _inputActions.Player.RightFire.performed += (ctx) => OnRightFirePress?.Invoke();
            _inputActions.Player.Dash.performed += (ctx) => OnDashPress?.Invoke();
            _inputActions.Player.Run.canceled += (ctx) => OnRunErased?.Invoke();
            _inputActions.Player.Run.performed += (ctx) => OnRunPress?.Invoke();
            _inputActions.Player.Utility.performed += (ctx) => OnUtilityPress?.Invoke();
            _inputActions.Player.PickUp.performed += (ctx) => OnPickUpPress?.Invoke();

            _inputActions.Enable();
        }

        public void ResetInput()
        {
            OnLeftFirePress = null;
            OnLeftFireErase = null;
            OnRightFirePress = null;
            OnDashPress = null;
            OnRunErased = null;
            OnRunPress = null;
            OnUtilityPress = null;
            OnPickUpPress = null;

            _inputActions.Disable();
        }
    }

    public interface IActionInput : IService
    {
        public event Action OnLeftFirePress;
        public event Action OnLeftFireErase;
        public event Action OnRightFirePress;
        public event Action OnDashPress;
        public event Action OnRunErased;
        public event Action OnRunPress;
        public event Action OnUtilityPress;
        public event Action OnPickUpPress;

    }

    public interface IValueInput : IService
    {
        public Vector2 MoveValue
        {
            get;
        }

        public Vector3 PlayerMouseWorldPos
        {
            get;
        }
    }

}
