using HalloGames.Architecture.Services;
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
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}

