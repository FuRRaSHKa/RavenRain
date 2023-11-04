using HalloGames.RavensRain.Gameplay.Player.Movement;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player
{

    public class PlayerInitilizer : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerRotator _playerRotator;

        public void InitPlayer(IInputService inputService)
        {
            _playerMovement.InitInput(inputService);
            _playerRotator.InitInput(inputService);
        }

    }

}
