using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon.Projectile
{
    public readonly struct ProjectileStruct
    {
        public readonly float Damage;
        public readonly float Speed;
        public readonly float LifeTime;
        public readonly LayerMask TargetLayer;
        public readonly string TargetTag;

        public ProjectileStruct(float damage, float speed, float lifeTime, LayerMask targetLayer, string targetTag)
        {
            Damage = damage;
            Speed = speed;
            LifeTime = lifeTime;
            TargetLayer = targetLayer;
            TargetTag = targetTag;
        }
    }


    public class Projectile
    {
        private readonly ProjectileStruct _projectileStruct;
        private readonly PoolObject _projectile;
        private readonly Vector3 _direction;

        private float _curLifeTime = 0;

        public LayerMask LayerMask => _projectileStruct.TargetLayer;
        public Vector3 Pos => _projectile.transform.position;

        public Projectile(ref ProjectileStruct projectileData, PoolObject projectile, Vector3 direction)
        {
            _projectileStruct = projectileData;
            _projectile = projectile;
            _direction = direction;
        }

        public void Move(out Vector3 direction, out float distance)
        {
            Vector3 delta = _direction * Time.deltaTime * _projectileStruct.Speed;
            direction = _direction;
            distance = Time.deltaTime * _projectileStruct.Speed;

            _projectile.transform.position += delta;
        }

        public bool HandleCollision(Collider collider)
        {
            if (collider.CompareTag(_projectileStruct.TargetTag) && collider.TryGetComponent(out IDamageable damageable))
                damageable.Damage(_projectileStruct.Damage);

            Destroy();
            return true;
        }

        public bool UpdateLifeTime()
        {
            _curLifeTime += Time.deltaTime;
            if (_curLifeTime <= _projectileStruct.LifeTime)
                return false;

            Destroy();
            return true;
        }

        private void Destroy()
        {
            _projectile.ForceDisable();
        }
    }
}

