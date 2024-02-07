using HalloGames.Architecture.PoolSystem;
using HalloGames.RavensRain.Gameplay.Player.Weapon;
using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Enemy
{
    public class RangeEnemyBrain : EnemyBrain
    {
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private float _shootDistance;


        private void Update()
        {
            if (Target == null)
            {
                _weaponController.DisableWeapon();
                return;
            }

            float curDistance = (Target.position - transform.position).magnitude;
            if (curDistance <= _shootDistance)
                _weaponController.EnableWeapon();
            else
                _weaponController.DisableWeapon();
        }
    }

    public abstract class EnemyBrain : MonoBehaviour
    {
        private Transform _target;

        protected Transform Target => _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}

