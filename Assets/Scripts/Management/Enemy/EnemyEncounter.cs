using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Enemy;
using HalloGames.RavensRain.Gameplay.Perk;
using HalloGames.RavensRain.Management.Factories;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Enemy
{
    public class EnemyEncounter : MonoBehaviour
    {
        [SerializeField] private LootBox[] _lootBoxes;
        [SerializeField] private PoolObject[] _possibleEnemies;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private string _playerTag;
        [SerializeField] private Collider _collider;

        private int _currentEnemyCount;

        private ICharacterFactory _characterFactory;

        private List<EnemyInitilizer> _entities = new List<EnemyInitilizer>();


        public void InitContext(ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        public void SpawnEnemies()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                int id = Random.Range(0, _possibleEnemies.Length);
                CharacterEntity characterEntity = _characterFactory.SpawnCharacter(_possibleEnemies[id]);
                characterEntity.transform.position = spawnPoint.position;

                _entities.Add(characterEntity.GetComponent<EnemyInitilizer>());
                characterEntity.CharacterHealth.OnDeathEvent += CharacterDeath;
            }

            _currentEnemyCount = _entities.Count;
        }

        private void CharacterDeath(CharacterEntity characterEntity)
        {
            characterEntity.CharacterHealth.OnDeathEvent -= CharacterDeath;
            _currentEnemyCount--;

            if (_currentEnemyCount <= 0)
                UnlockLoot();
        }

        private void UnlockLoot()
        {
            foreach (var lootBox in _lootBoxes)
            {
                lootBox.SpawnLoot();
            }

            _entities.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_playerTag))
                return;

            _collider.enabled = false;
            foreach (var entity in _entities)
            {
                entity.SetTarget(other.transform);
            }
        }
    }
}


