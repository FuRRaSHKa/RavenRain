using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class LevelContextBuilder : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;

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
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}

