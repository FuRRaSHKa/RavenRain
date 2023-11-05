using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterEntity _character;

        private float _maxHealth;
        private float _currentHealth;

        private bool _isAlive;

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        public event Action OnDeathEvent;
        public event Action OnHealthChange;

        private void Awake()
        {
            _character.CharacterDataWrapper.OnStatChanged += UpdateValue;
        }

        private void Start()
        {
            Rewive();
        }

        public void Rewive()
        {
            _isAlive = true;
            UpdateValue(StatTypesEnum.Health);
            _currentHealth = _maxHealth;

            OnHealthChange?.Invoke();
        }

        private void UpdateValue(StatTypesEnum targetType)
        {
            if (targetType != StatTypesEnum.Health)
                return;

            _maxHealth = _character.CharacterDataWrapper.GetValue(StatTypesEnum.Health);
            OnHealthChange?.Invoke();
        }

        public void Damage(float damage)
        {
            if (!_isAlive)
                return;

            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(damage, 0, _maxHealth);

            if (_currentHealth == 0)
            {
                _isAlive = false;
                OnDeathEvent?.Invoke();
            }

            OnHealthChange?.Invoke();
        }
    }

    public interface IDamageable
    {
        public void Damage(float damage);
    }

}

