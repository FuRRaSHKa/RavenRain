using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Player.Movement;
using HalloGames.RavensRain.Gameplay.Player.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player
{
    public class PlayerEntity : CharacterEntity
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerWeaponController _weaponController;

        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerWeaponController WeaponController => _weaponController;
    }
}

