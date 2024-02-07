using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public class WeaponDataWrapper : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _characterEntity;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private string _targetTag;

        private WeaponData _weaponData;

        private float _currentDamage;
        private float _currentCrit;
        private float _roF; 

        public int BulletAmmount => _weaponData.BulletsAmmount;
        public float RandomDirectionCoeff => _weaponData.RandomDirectionCoeff;
        public float RoF => _roF;

        public event Action OnDataUpdated;
        public event Action<DescriptionStruct> OnWeaponChanged;

        private void Awake()
        {
            _characterEntity.CharacterDataWrapper.OnStatChanged += CalculateStats;
        }

        public WeaponData SetWeaponData(WeaponData weaponData, bool showNotify = true)
        {
            WeaponData tempWeapon = _weaponData;
            _weaponData = weaponData;
            CalculateStats(StatTypesEnum.Damage);
            CalculateStats(StatTypesEnum.RoF);
            CalculateStats(StatTypesEnum.Crit);

            if (showNotify)
                OnWeaponChanged?.Invoke(weaponData.DescriptionStruct);

            return tempWeapon;
        }

        private void CalculateStats(StatTypesEnum targetValue)
        {
            if (_weaponData == null)
                return;

            if (targetValue == StatTypesEnum.Damage || targetValue == StatTypesEnum.All)
                _currentDamage = _characterEntity.CharacterDataWrapper.GetValue(StatTypesEnum.Damage) * _weaponData.DamageCoeff;

            if (targetValue == StatTypesEnum.RoF || targetValue == StatTypesEnum.All)
            {
                _roF = _characterEntity.CharacterDataWrapper.GetValue(StatTypesEnum.RoF) * _weaponData.RateOfFire;
                OnDataUpdated?.Invoke();
            }

             if(targetValue == StatTypesEnum.Crit || targetValue == StatTypesEnum.All)
                _currentCrit = _characterEntity.CharacterDataWrapper.GetValue(StatTypesEnum.Crit);
        }

        public ProjectileStruct GetWeaponStruct()
        {
            return new ProjectileStruct(_currentDamage, _weaponData.BulletSpeed, _weaponData.LifeTime ,_targetLayer, _targetTag, _currentCrit);
        }
    }
}