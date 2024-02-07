using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Enemy
{
    public class EnemyWeaponController : WeaponController
    {
        private Transform _target;

        private void Start()
        {
            InstallContex(GetComponentInChildren<IShooter>());
        }

        public void InstallContex(Transform target)
        {
            _target = target;
        }

        protected override Vector3 GetTargetPos()
        {
            return _target.position;
        }
    }
}


