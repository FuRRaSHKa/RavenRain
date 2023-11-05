using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using HalloGames.RavensRain.Management.Factories;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public class ProjectileShooter : MonoBehaviour, IShooter
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private PoolObject _projectilePrefab;

        private IProjectileFactory _projectileFactory;

        public void InitProjectileFactory(IProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }

        public void Shoot(Vector3 targetPos, ref ProjectileStruct projectileStruct, int projectileCount, float randomDirectionCoeff)
        {
            Vector3 direction = (targetPos - _shootPoint.position);
            direction.y = 0;
            direction = direction.normalized;

            for (int i = 0; i < projectileCount; i++)
            {
                float angle = Random.Range(-10, 10) * randomDirectionCoeff;
                Quaternion randomRotation = Quaternion.Euler(0, angle, 0);
                direction = randomRotation * direction;

                _projectileFactory.SpawnProjectile(_shootPoint.position, direction, ref projectileStruct, _projectilePrefab);
            }
        }
    }
}
