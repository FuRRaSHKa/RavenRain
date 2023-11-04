using HalloGames.RavensRain.Gameplay.Player.Movement;
using HalloGames.RavensRain.Gameplay.Player.States;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player
{

    public class PlayerInitilizer : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerRotator _playerRotator;
        [SerializeField] private PlayerInputController _playerInputController;

        public void InitPlayer(IActionInput actionInput, IValueInput valueInput)
        {
            _playerMovement.InitInput(valueInput);
            _playerRotator.InitInput(valueInput);
            _playerInputController.InitInput(actionInput);
        }

    }

}
