using HalloGames.Architecture.PoolSystem;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Weapon.Projectile
{
    public class BulletVisual : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        public void ClearTrail()
        {
           _trailRenderer.Clear();
        }
    }

}

