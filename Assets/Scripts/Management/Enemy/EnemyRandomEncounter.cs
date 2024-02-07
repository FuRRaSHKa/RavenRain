using HalloGames.Architecture.CoroutineManagement;
using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Player;
using HalloGames.RavensRain.Management.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Enemy
{
    public class EnemyRandomEncounter : MonoBehaviour
    {
        [SerializeField] private PoolObject[] _enemies;
        [SerializeField] private PerkData[] _rewards;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _spawnCount;
        [SerializeField] private float _distance; 
        [SerializeField] private PlayerEntity _player;

        private int _currentCount;

        private Transform _closestSpawnPoint;

        private ICharacterFactory _characterFactory;
        private IStopable _stopable;

        private List<CharacterEntity> _entities = new List<CharacterEntity>();

        public void InstallContext(ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;

            Wait();
        }

        private void FindClosestSpawnPoint()
        {
            _closestSpawnPoint = _spawnPoints.OrderBy(o => (o.position - _player.transform.position).sqrMagnitude).FirstOrDefault();
        }

        private Vector3 GetSpawnPoint()
        {
            Vector3 spawnPoint = Random.onUnitSphere.normalized;
            spawnPoint.y = 0;
            spawnPoint += _closestSpawnPoint.transform.position;
            
            return spawnPoint;
        }

        public void StopSpawning()
        {
            _stopable?.Stop();
        }

        private void Wait()
        {
            _stopable = RoutineManager.CreateRoutine(this).Wait(_spawnDelay, SpawnEnemies).Start(); 
        }

        private void SpawnEnemies()
        {
            FindClosestSpawnPoint();

            for (int i = 0; i < _spawnCount; i++)
            {
                int id = Random.Range(0, _enemies.Length);
                CharacterEntity characterEntity = _characterFactory.SpawnCharacter(_enemies[id]);
                characterEntity.transform.position = GetSpawnPoint();

                _entities.Add(characterEntity);
                characterEntity.CharacterHealth.OnDeathEvent += CharacterDeath;
            }

            _stopable = RoutineManager.CreateRoutine(this).NextFrame(() =>
            {
                foreach (var entity in _entities)
                {
                    entity.GetComponent<EnemyInitilizer>().SetTarget(_player.transform);
                }
            }).Start();

            _currentCount = _entities.Count;
            _spawnCount++;
        }

        private void CharacterDeath(CharacterEntity characterEntity)
        {
            characterEntity.CharacterHealth.OnDeathEvent -= CharacterDeath;
            _currentCount--;

            _entities.Remove(characterEntity);

            if (_currentCount <= 0)
                GiveLoot();
        }

        private void GiveLoot()
        {
            PerkData perkData = _rewards[Random.Range(0, _rewards.Length)];
            IPerk perk = perkData.GetPerk(_player);
            _player.PerkHandler.AddPerk(perkData.name, perk);

            Wait(); 
        }

    }
}


