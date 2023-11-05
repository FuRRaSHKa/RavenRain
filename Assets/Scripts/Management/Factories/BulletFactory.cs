using HalloGames.Architecture.PoolSystem;
using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using HalloGames.RavensRain.Management.ProjectileManagement;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Factories
{
    public class BulletFactory : IProjectileFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BulletFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SpawnProjectile(Vector3 origin, Vector3 direction, ref ProjectileStruct projectileData, PoolObject prefab)
        {
            PoolObject poolObject = PoolManager.Instance[prefab].SpawnObject();
            poolObject.transform.position = origin;
            poolObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            poolObject.GetComponent<BulletVisual>().ClearTrail();

            Projectile projectile = new Projectile(ref projectileData, poolObject, direction);
            _serviceProvider.GetService<ProjectileSystem>().AddProjectile(projectile);
        }
    }

    public interface IProjectileFactory : IService
    {
        public void SpawnProjectile(Vector3 origin, Vector3 direction, ref ProjectileStruct projectileData, PoolObject prefab);
    }

}
