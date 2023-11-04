using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Weapon
{
    public class PlayerWeaponController : MonoBehaviour, IWeaponController
    {
        public void DisableWeapon()
        {
            throw new System.NotImplementedException();
        }

        public void EnableWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public interface IWeaponController
    {
        public void EnableWeapon();

        public void DisableWeapon();
    }
}


