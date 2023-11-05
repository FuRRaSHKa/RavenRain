using HalloGames.RavensRain.Gameplay.Weapon;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Weapon
{
    public class PlayerWeaponInitilizer : MonoBehaviour
    {
        [SerializeField] private WeaponData _defaultWeaponData;
        [SerializeField] private PlayerWeaponController _weaponController;
        [SerializeField] private ProjectileShooter _projectileShooter;
        [SerializeField] private WeaponDataWrapper _dataWrapper;

        public void InitWeapon(IProjectileFactory projectileFactory, IValueInput valueInput)
        {
            _projectileShooter.InitProjectileFactory(projectileFactory);
            _weaponController.InstallContex(_projectileShooter, valueInput);
            _dataWrapper.SetWeaponData(_defaultWeaponData);
        }
    }

}
