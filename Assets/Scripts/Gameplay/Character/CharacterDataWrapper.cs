using HalloGames.RavensRain.Gameplay.Characters.Stats;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    public class CharacterDataWrapper : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;

        private int _currentLevel;

        private Dictionary<StatTypesEnum, Dictionary<string, IStatModifyer>> _statMods;

        public event Action<StatTypesEnum> OnStatChanged;

        private void Awake()
        {
            _statMods = new Dictionary<StatTypesEnum, Dictionary<string, IStatModifyer>>()
            {
                { StatTypesEnum.Health, new Dictionary<string, IStatModifyer>()},
                { StatTypesEnum.Speed, new Dictionary<string, IStatModifyer>() },
                { StatTypesEnum.Damage, new Dictionary<string, IStatModifyer>()},
                { StatTypesEnum.Armor, new Dictionary<string, IStatModifyer>() },
                { StatTypesEnum.RoF, new Dictionary<string, IStatModifyer>()}
            };
        }

        public float GetValue(StatTypesEnum statType)
        {
            float value = GetRawValue(statType);
            var mods = _statMods[statType].Values.ToList();
            mods.ForEach(f => value += f.ModifyStat(value));
            return value;
        }

        public void AddMod(StatTypesEnum targetValue, string key, IStatModifyer statModifyer)
        {
            _statMods[targetValue].Add(key, statModifyer);
            OnStatChanged?.Invoke(targetValue);
        }

        public void RemoveMod(StatTypesEnum targetValue, string key)
        {
            _statMods[targetValue].Remove(key);
            OnStatChanged?.Invoke(targetValue);
        }

        public float GetRawValue(StatTypesEnum statTypesEnum)
        {
            switch (statTypesEnum)
            {
                case StatTypesEnum.Health:
                    return _characterData.BaseMaxHealth + _characterData.HealthPerLevel * _currentLevel;

                case StatTypesEnum.Speed:
                    return _characterData.BaseSpeed + _characterData.SpeedPerLevel * _currentLevel;

                case StatTypesEnum.Damage:
                    return _characterData.BaseDamage + _characterData.DamagePerLevel * _currentLevel;

                case StatTypesEnum.Armor:
                    return _characterData.BaseArmor + _characterData.ArmorPerLevel * _currentLevel;

                case StatTypesEnum.RoF:
                    return _characterData.BaseRoF + _characterData.RoFPerLevel * _currentLevel;
            }

            return 0;
        }

    }
}

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public enum StatTypesEnum
    {
        Health,
        Speed,
        Damage,
        Armor,
        RoF
    }
}
