using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Weapon;
using HalloGames.RavensRain.Management.Factories;
using UnityEngine;


namespace HalloGames.RavensRain.Gameplay.Enemy
{
    public class EnemyInitilizer : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private EnemyWeaponController _controller;
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyBrain _enemyBrain;
        [SerializeField] private ProjectileShooter _projectileShooter;
        [SerializeField] private CharacterEntity _characterEntity;
        [SerializeField] private PoolObject _poolObject;

        private void Awake()
        {
            _characterEntity.CharacterHealth.OnDeathEvent += OnDeath;
        }

        public void Initilize(IProjectileFactory projectileFactory)
        {
            if (_projectileShooter != null)
                _projectileShooter.InitProjectileFactory(projectileFactory);

            _characterEntity.WeaponDataWrapper.SetWeaponData(_weaponData);
            _characterEntity.CharacterHealth.Rewive();
        }

        public void SetTarget(Transform target)
        {
            _controller.InstallContex(target);
            _movementController.SetTarget(target);
            _enemyBrain.SetTarget(target);
        }

        private void OnDeath(CharacterEntity characterEntity)
        {
            _controller.InstallContex(null);
            _movementController.SetTarget(null);
            _enemyBrain.SetTarget(null);

            _poolObject.ForceDisable();
        }
    }
}

