using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private float _damageCoeff;
        [SerializeField] private float _bulletsAmmount;
        [SerializeField] private float _randomDirectionCoeff;
        [SerializeField] private float _rateOfFire;
    
        public float DamageCoeff => _damageCoeff;
        public float BulletsAmmount => _bulletsAmmount;
        public float RandomDirectionCoeff => _randomDirectionCoeff;
        public float RateOfFire => _rateOfFire;
    }
}