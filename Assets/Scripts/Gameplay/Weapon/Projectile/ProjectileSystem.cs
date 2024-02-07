using HalloGames.Architecture.Services;
using HalloGames.RavensRain.Gameplay.Weapon.Projectile;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


namespace HalloGames.RavensRain.Management.ProjectileManagement
{
    public class ProjectileSystem : MonoBehaviour, IService
    {
        private List<Projectile> _projectiles = new List<Projectile>();

        private NativeArray<RaycastCommand> _raycasts;
        private JobHandle _raycastJob;
        private NativeArray<RaycastHit> _results;

        public void AddProjectile(Projectile projectile)
        {
            _projectiles.Add(projectile);
        }

        private void Update()
        {
            SheludeMoving();
            Raycasts();
        }

        private void LateUpdate()
        {
            CompleteRaycasts();
            SheludeLifeTime();
            ClearData();
        }

        private void SheludeMoving()
        {
            _raycasts = new NativeArray<RaycastCommand>(_projectiles.Count, Allocator.TempJob);

            for (int i = 0; i < _projectiles.Count; i++)
            {
                Vector3 origin = _projectiles[i].Pos;
                _projectiles[i].Move(out Vector3 direction, out float distance);

                RaycastCommand raycastCommand = new RaycastCommand(origin, direction, new QueryParameters(_projectiles[i].LayerMask.value), distance);
                _raycasts[i] = raycastCommand;
            }
        }

        private void Raycasts()
        {
            _results = new NativeArray<RaycastHit>(_projectiles.Count, Allocator.TempJob);

            _raycastJob = RaycastCommand.ScheduleBatch(_raycasts, _results, 1);
        }

        private void CompleteRaycasts()
        {
            _raycastJob.Complete();

            for (int i = 0, j = 0; i < _results.Length && j < _projectiles.Count; j++, i++)
            {
                if (_results[i].collider == null)
                    continue;

                if (_projectiles[j].HandleCollision(_results[i].collider))
                {
                    _projectiles.RemoveAt(j);
                    j--;
                }
            }
        }

        private void SheludeLifeTime()
        {

            for (int i = 0; i < _projectiles.Count; i++)
            {

                if (_projectiles[i].UpdateLifeTime())
                {
                    _projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        private void ClearData()
        {
            _raycasts.Dispose();
            _results.Dispose();
        }

    }

}

