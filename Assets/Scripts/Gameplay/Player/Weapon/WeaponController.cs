using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Utils;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public abstract class WeaponController : MonoBehaviour, IWeaponController
    {
        [SerializeField] private CharacterEntity _characterEntity;

        private CoolDownController _coolDownController;
        private IShooter _shooter;
  

        private bool _enableToShoot = false;

        private void Awake()
        {
            _coolDownController = new CoolDownController();
            _characterEntity.WeaponDataWrapper.OnDataUpdated += UpdateValues;
        }

        private void UpdateValues()
        {
            float time = 1 / (_characterEntity.WeaponDataWrapper.RoF);
            _coolDownController.SetTime(time);  
        }

        protected void InstallContex(IShooter shooter)
        {
            _shooter = shooter;
        }

        public void DisableWeapon()
        {
            _enableToShoot = false;
        }

        public void EnableWeapon()
        {
            _enableToShoot = true;
        }

        protected abstract Vector3 GetTargetPos();

        private void Update()
        {
            _coolDownController.Update();

            if (_enableToShoot && _coolDownController.IsCooldownEnded())
            {
                Vector3 targetPos = GetTargetPos();
                ProjectileStruct projectileStruct = _characterEntity.WeaponDataWrapper.GetWeaponStruct();

                _shooter.Shoot(targetPos, ref projectileStruct, _characterEntity.WeaponDataWrapper.BulletAmmount, _characterEntity.WeaponDataWrapper.RandomDirectionCoeff);
                _coolDownController.StartCooldown();
            }
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


