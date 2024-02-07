using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Management.Enemy;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using HalloGames.RavensRain.Management.ProjectileManagement;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class LevelContextBuilder : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private ProjectileSystem _projectileSystem;
        [SerializeField] private EnemyContainer _enemyContainer;
        [SerializeField] private EnemyLevelManager _enemyLevelManager;

        private ServiceProvider _serviceProvider;

        private void Awake()
        {
            BuildServices();
        }

        private void BuildServices()
        {
            _serviceProvider = new ServiceProvider();

            _serviceProvider.AddService<IValueInput>(_inputController);
            _serviceProvider.AddService<IActionInput>(_inputController);
            _serviceProvider.AddService(_projectileSystem);

            BulletFactory bulletFactory = new BulletFactory(_serviceProvider);
            _serviceProvider.AddService<IProjectileFactory>(bulletFactory);

            EnemyFactory enemyFactory = new EnemyFactory(_serviceProvider);
            _serviceProvider.AddService<ICharacterFactory>(enemyFactory);

            _serviceProvider.AddService<IEnemyContainer>(_enemyContainer);
            _serviceProvider.AddService<IEnemyLevelManager>(_enemyLevelManager);
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}

