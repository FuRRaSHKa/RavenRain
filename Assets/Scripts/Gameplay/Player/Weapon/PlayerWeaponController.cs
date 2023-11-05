using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Utils;
using HalloGames.RavensRain.Gameplay.Weapon;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Weapon
{
    public class PlayerWeaponController : MonoBehaviour, IWeaponController
    {
        [SerializeField] private CharacterEntity _characterEntity;

        private CoolDownController _coolDownController;
        private IShooter _shooter;
        private IValueInput _valueInput;

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

        public void InstallContex(IShooter shooter, IValueInput valueInput)
        {
            _shooter = shooter;
            _valueInput = valueInput;
        }

        public void DisableWeapon()
        {
            _enableToShoot = false;
        }

        public void EnableWeapon()
        {
            _enableToShoot = true;
        }

        private void Update()
        {
            _coolDownController.Update();

            if (_enableToShoot && _coolDownController.IsCooldownEnded())
            {
                Vector3 targetPos = _valueInput.PlayerMouseWorldPos;
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


