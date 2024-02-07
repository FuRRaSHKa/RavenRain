using HalloGames.RavensRain.Gameplay.Weapon;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Weapon
{
    public class PlayerWeaponController : WeaponController
    {
        private IValueInput _valueInput;

        protected override Vector3 GetTargetPos()
        {
            return _valueInput.PlayerMouseWorldPos;
        }

        public void InstallContex(IShooter shooter, IValueInput valueInput)
        {
            InstallContex(shooter);
            _valueInput = valueInput;
        }
    }
}