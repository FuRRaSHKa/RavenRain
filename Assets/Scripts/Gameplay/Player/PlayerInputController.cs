using HalloGames.RavensRain.Gameplay.Player.Movement;
using HalloGames.RavensRain.Gameplay.Player.States;
using HalloGames.RavensRain.Gameplay.Player.Weapon;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerStateController _stateController;
    [SerializeField] private PlayerMovement _playerMovement;
    
    private IActionInput _actionInput;

    private void Awake()
    {
        _playerMovement.OnDashEnd += DashEnded;
    }

    public void InitInput(IActionInput actionInput)
    {
        _actionInput = actionInput;

        _actionInput.OnRunPress += RunPressed;
        _actionInput.OnRunErased += RunErased;
        _actionInput.OnDashPress += DashPressed;

        _actionInput.OnLeftFirePress += FirePressed;
        _actionInput.OnLeftFireErase += FireErased;
    }

    private void FirePressed()
    {
        _stateController.AddState(typeof(FireState));
    }

    private void FireErased()
    {
        _stateController.RemoveState(typeof(FireState));
    }

    private void RunPressed()
    {
        _stateController.AddState(typeof(RunState));
    }

    private void RunErased()
    {
        _stateController.RemoveState(typeof(RunState));
    }

    private void DashPressed()
    {
        _stateController.AddState(typeof(DashState));
    }

    private void DashEnded()
    {
        _stateController.RemoveState(typeof(DashState));
    }
}
