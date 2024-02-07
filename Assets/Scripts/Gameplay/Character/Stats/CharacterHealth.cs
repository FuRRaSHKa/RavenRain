using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterEntity _character;
        [SerializeField] private bool _restore;

        private float _maxHealth;
        private float _currentHealth;
        private float _armor;

        private bool _isAlive;

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        public event Action<CharacterEntity> OnDeathEvent;
        public event Action OnHealthChange;
        public event Action OnDamage;

        private void Awake()
        {
            _character.CharacterDataWrapper.OnStatChanged += UpdateValueHealht;
        }

        private void Start()
        {
            Rewive();
        }

        private void Update()
        {
            if (_restore && _currentHealth < _maxHealth)
            {
                _currentHealth += 1 * Time.deltaTime;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
                OnHealthChange?.Invoke();
            }

        }

        public void Rewive()
        {
            _isAlive = true;
            UpdateValueHealht(StatTypesEnum.Health);
            UpdateValueArmor(StatTypesEnum.Armor);
            _currentHealth = _maxHealth;

            OnHealthChange?.Invoke();
        }

        private void UpdateValueArmor(StatTypesEnum targetType)
        {
            if (targetType != StatTypesEnum.Armor && targetType != StatTypesEnum.All)
                return;

            _armor = _character.CharacterDataWrapper.GetValue(StatTypesEnum.Armor);
        }

        private void UpdateValueHealht(StatTypesEnum targetType)
        {
            if (targetType != StatTypesEnum.Health && targetType != StatTypesEnum.All)
                return;

            _maxHealth = _character.CharacterDataWrapper.GetValue(StatTypesEnum.Health);
            OnHealthChange?.Invoke();
        }

        public void Damage(float damage)
        {
            if (!_isAlive)
                return;

            damage = Mathf.Clamp(damage - _armor, 1f, damage);

            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, -1, _maxHealth);

            if (_currentHealth <= 0)
            {
                _isAlive = false;
                OnDeathEvent?.Invoke(_character);
            }

            OnDamage?.Invoke();

            OnHealthChange?.Invoke();
        }
    }

    public interface IDamageable
    {
        public void Damage(float damage);
    }

}

