
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon
{
    public interface IShooter 
    {
        public void Shoot(Vector3 targetPos, ref ProjectileStruct projectileStruct, int projectileCount, float randomDirectionCoeff);
    }
}
