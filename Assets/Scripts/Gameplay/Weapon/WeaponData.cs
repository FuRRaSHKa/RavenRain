using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private float _damageCoeff;
        [SerializeField] private int _bulletsAmmount;
        [SerializeField, Range(0, 1)] private float _randomDirectionCoeff;
        [SerializeField] private float _rateOfFire;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _bulletSpeed;

        public float DamageCoeff => _damageCoeff;
        public int BulletsAmmount => _bulletsAmmount;
        public float RandomDirectionCoeff => _randomDirectionCoeff;
        public float RateOfFire => _rateOfFire;
        public float LifeTime => _lifeTime;
        public float BulletSpeed => _bulletSpeed;
    }
}