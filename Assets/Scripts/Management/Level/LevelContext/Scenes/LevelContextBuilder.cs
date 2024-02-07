using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Management.Enemy;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using HalloGames.RavensRain.Management.ProjectileManagement;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class LevelContextBuilder : ContextBuilder
    {
        [SerializeField] private EnemyContainer _enemyContainer;
        [SerializeField] private EnemyLevelManager _enemyLevelManager;

        protected override void BuildServices()
        {
            base.BuildServices();

            BulletFactory bulletFactory = new BulletFactory(ServiceProvider);
            ServiceProvider.AddService<IProjectileFactory>(bulletFactory);

            EnemyFactory enemyFactory = new EnemyFactory(ServiceProvider);
            ServiceProvider.AddService<ICharacterFactory>(enemyFactory);

            ServiceProvider.AddService<IEnemyContainer>(_enemyContainer);
            ServiceProvider.AddService<IEnemyLevelManager>(_enemyLevelManager);
        }
    }
}

