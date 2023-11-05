using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
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
        private float _roF; 

        public int BulletAmmount => _weaponData.BulletsAmmount;
        public float RandomDirectionCoeff => _weaponData.RandomDirectionCoeff;
        public float RoF => _roF;

        public event Action OnDataUpdated;
        
        private void Awake()
        {
            _characterEntity.CharacterDataWrapper.OnStatChanged += CalculateStats;
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
            CalculateStats(StatTypesEnum.Damage);
            CalculateStats(StatTypesEnum.RoF);
        }

        private void CalculateStats(StatTypesEnum targetValue)
        {
            if (_weaponData == null)
                return;

            if (targetValue == StatTypesEnum.Damage)
                _currentDamage = _characterEntity.CharacterDataWrapper.GetValue(StatTypesEnum.Damage) * _weaponData.DamageCoeff;
            if (targetValue == StatTypesEnum.RoF)
            {
                _roF = _characterEntity.CharacterDataWrapper.GetValue(StatTypesEnum.RoF) * _weaponData.RateOfFire;
                OnDataUpdated?.Invoke();
            }
        }

        public ProjectileStruct GetWeaponStruct()
        {
            return new ProjectileStruct(_currentDamage, _weaponData.BulletSpeed, _weaponData.LifeTime ,_targetLayer, _targetTag);
        }
    }
}