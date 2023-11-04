using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Player.Movement;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player
{
    public class PlayerEntity : CharacterEntity
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public PlayerMovement PlayerMovement => _playerMovement;
    }
}

