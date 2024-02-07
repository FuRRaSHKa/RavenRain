using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HalloGames.RavensRain.Gameplay.Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _distanceToPlayer;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
            _navMeshAgent.enabled = true;
        }

        private void OnDisable()
        {
            _navMeshAgent.enabled = false;
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;

            Vector3 dir = (transform.position - _target.position).normalized;
            Vector3 pos = _target.position + dir * _distanceToPlayer;
            _navMeshAgent.SetDestination(pos);
        }
    }
}


