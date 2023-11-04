using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Base values")]
        [SerializeField] private float _baseMaxHealth;
        [SerializeField] private float _baseArmor;
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseRoF;
        [SerializeField] private float _xpToLevel;

        [Header("Progress")]
        [SerializeField] private float _healthPerLevel;
        [SerializeField] private float _armorPerLevel;
        [SerializeField] private float _speedPerLevel;
        [SerializeField] private float _damagePerLevel;
        [SerializeField] private float _roFPerLevel;
        [SerializeField] private float _xpIncrease;

        public float BaseMaxHealth => _baseMaxHealth;
        public float BaseArmor => _baseArmor;
        public float BaseSpeed => _baseSpeed;    
        public float BaseDamage => _baseDamage;
        public float BaseRoF => _baseRoF;
        public float XPToLevel => _xpToLevel;
        public float HealthPerLevel => _healthPerLevel;
        public float ArmorPerLevel => _armorPerLevel;
        public float SpeedPerLevel => _speedPerLevel;
        public float DamagePerLevel => _damagePerLevel;
        public float RoFPerLevel => _roFPerLevel;
        public float XPIncrease => _xpIncrease; 
    }
}


