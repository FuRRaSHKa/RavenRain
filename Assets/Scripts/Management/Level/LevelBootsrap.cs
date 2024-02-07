using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Player;
using HalloGames.RavensRain.Management.Enemy;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{

    public class LevelBootsrap : MonoBehaviour
    {
        [SerializeField] private LevelContextBuilder _contextBuilder;
        [SerializeField] private PlayerInitilizer _playerInitilizer;
        [SerializeField] private EncounterManager _encounterManager;
        [SerializeField] private EnemyLevelManager _enemyLevelManager;

        private IServiceProvider _serviceProvider;

        private void Start()
        {
            _serviceProvider = _contextBuilder.GetServiceProvider();
            
            InitGame();
        }

        private void InitGame()
        {
            _enemyLevelManager.SetContainer(_serviceProvider.GetService<IEnemyContainer>());    
            _playerInitilizer.InitPlayer(_serviceProvider.GetService<IActionInput>(), _serviceProvider.GetService<IValueInput>(), _serviceProvider.GetService<IProjectileFactory>());
            _encounterManager.SetService(_serviceProvider.GetService<ICharacterFactory>());
        }
    }

}

