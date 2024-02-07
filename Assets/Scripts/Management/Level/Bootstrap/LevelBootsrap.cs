using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Player;
using HalloGames.RavensRain.Management.Enemy;
using HalloGames.RavensRain.Management.Factories;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class LevelBootsrap : Bootsrap
    {
        [SerializeField] private ContextBuilder _contextBuilder;
        [SerializeField] private PlayerInitilizer _playerInitilizer;
        [SerializeField] private EncounterManager _encounterManager;
        [SerializeField] private EnemyLevelManager _enemyLevelManager;

        protected override void InitGame()
        {
            base.InitGame();

            _enemyLevelManager.SetContainer(ServiceProvider.GetService<IEnemyContainer>());   
            _encounterManager.SetService(ServiceProvider.GetService<ICharacterFactory>());
        }
    }

}

