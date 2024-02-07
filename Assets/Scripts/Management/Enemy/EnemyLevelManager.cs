using HalloGames.Architecture.CoroutineManagement;
using HalloGames.Architecture.Services;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Enemy
{
    public class EnemyLevelManager : MonoBehaviour, IEnemyLevelManager
    {
        [SerializeField] private float _levelTime;

        private IEnemyContainer _container;

        private int _level = 1;

        public int Level => _level;

        private void Awake()
        {
            RoutineManager.CreateRoutine(this).RepeatWaiting(_levelTime, LevelUp).Start();
        }

        public void SetContainer(IEnemyContainer enemyContainer)
        {
            _container = enemyContainer;
        }

        private void LevelUp()
        {
            _level++;

            var entities = _container.CharacterEntities;
            foreach (var entity in entities)
            {
                entity.CharacterDataWrapper.SetLevel(Level);
            }
        }
    }

    public interface IEnemyLevelManager : IService
    {
        public int Level
        {
            get;
        }
    }
}


