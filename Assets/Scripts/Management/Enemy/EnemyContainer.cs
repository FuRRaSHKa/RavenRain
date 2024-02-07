using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Enemy
{
    public class EnemyContainer : MonoBehaviour, IEnemyContainer
    {
        private List<CharacterEntity> _characterEntities = new List<CharacterEntity>();

        public List<CharacterEntity> CharacterEntities => _characterEntities;

        public event Action OnEnemyDeath;

        public void AddEnemy(CharacterEntity characterEntity)
        {
            _characterEntities.Add(characterEntity);
            characterEntity.CharacterHealth.OnDeathEvent += EmemyDeath;
        }

        private void EmemyDeath(CharacterEntity characterEntity)
        {
            characterEntity.CharacterHealth.OnDeathEvent -= EmemyDeath;
            OnEnemyDeath?.Invoke();
        }

        public void RemoveAllEnemies()
        {
            foreach(var characterEntity in _characterEntities)
            {
                characterEntity.CharacterHealth.OnDeathEvent -= EmemyDeath; 
            }

            _characterEntities.Clear();
        }
    }

    public interface IEnemyContainer : IService
    {
        public List<CharacterEntity> CharacterEntities
        {
            get;
        }

        public void AddEnemy(CharacterEntity characterEntity);
    }

}
