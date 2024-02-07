using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Player;
using HalloGames.RavensRain.Management.Enemy;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;


namespace HalloGames.RavensRain.Management.Level
{

    public class Bootsrap : MonoBehaviour
    {
        [SerializeField] private ContextBuilder _contextBuilder;
        [SerializeField] private PlayerInitilizer _playerInitilizer;

        private IServiceProvider _serviceProvider;

        protected IServiceProvider ServiceProvider => _serviceProvider;

        private void Start()
        {
            _serviceProvider = _contextBuilder.GetServiceProvider();

            InitGame();
        }

        protected virtual void InitGame()
        {
            _playerInitilizer.InitPlayer(_serviceProvider.GetService<IActionInput>(), _serviceProvider.GetService<IValueInput>(), _serviceProvider.GetService<IProjectileFactory>());
        }
    }

}