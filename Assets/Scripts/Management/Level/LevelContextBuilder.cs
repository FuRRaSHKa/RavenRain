using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class LevelContextBuilder : MonoBehaviour
    {
        [SerializeField] private PlayerInputController _inputController;

        private ServiceProvider _serviceProvider;

        private void Awake()
        {
            BuildServices();
        }

        private void BuildServices()
        {
            _serviceProvider = new ServiceProvider();

            _serviceProvider.AddService<IInputService>(_inputController);
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}

