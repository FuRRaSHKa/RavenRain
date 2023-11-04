using HalloGames.RavensRain.Gameplay.Characters;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public class WeaponDataWrapper : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _characterEntity;

        private WeaponData _weaponData;

        private float _currentDamage;
        private float _currentRoF;

        private void Awake()
        {
            _characterEntity.CharacterDataWrapper.OnStatChanged += (stat) => CalculateStats();
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
            CalculateStats();
        }

        private void CalculateStats()
        {
            if (_weaponData == null)
                return;

            _currentDamage = _characterEntity.CharacterDataWrapper.GetValue(Characters.Stats.StatTypesEnum.Damage) * _weaponData.DamageCoeff;
            _currentRoF = _characterEntity.CharacterDataWrapper.GetValue(Characters.Stats.StatTypesEnum.RoF) * _weaponData.RateOfFire;
        }
    }
}