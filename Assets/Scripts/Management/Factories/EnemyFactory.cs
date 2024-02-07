using HalloGames.Architecture.PoolSystem;
using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Enemy;
using HalloGames.RavensRain.Management.Enemy;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Factories
{
    public class EnemyFactory : ICharacterFactory
    {
        private IServiceProvider _serviceProvider;

        public EnemyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CharacterEntity SpawnCharacter(PoolObject prefab)
        {
            CharacterEntity characterEntity = PoolManager.Instance[prefab].SpawnObject().GetComponent<CharacterEntity>();
            characterEntity.GetComponent<EnemyInitilizer>().Initilize(_serviceProvider.GetService<IProjectileFactory>());
            characterEntity.CharacterDataWrapper.SetLevel(_serviceProvider.GetService<IEnemyLevelManager>().Level);

            _serviceProvider.GetService<IEnemyContainer>().AddEnemy(characterEntity);

            return characterEntity;
        }
    }

    public interface ICharacterFactory : IService
    {
        public CharacterEntity SpawnCharacter(PoolObject prefab);
    }
}


