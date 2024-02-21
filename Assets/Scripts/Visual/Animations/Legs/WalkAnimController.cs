using HalloGames.Architecture.CoroutineManagement;
using HalloGames.RavensRain.Visuals.Animations.IK;
using HalloGames.RavensRain.Visuals.Animations.IK.Raycasts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Visuals.Animations.Walk
{

    public class WalkAnimController : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _stepRadius;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _moveThreshold;

        [Header("Step Setttings")]
        [SerializeField] private float _stepDuration;
        [SerializeField] private AnimationCurve _stepYCurve;
        [SerializeField] private AnimationCurve _moveCurve;
        [SerializeField] private float _yMod;

        [Header("Legs Setttings")]
        [SerializeField] private LegIKData[] _legIKDatas;

        private LegIKController[] _legIKs;
        private IkRaycaster _raycaster;

        private bool _isMoving;
        private Vector3 _prevPos;

        private void Awake()
        {
            InitLegs();

            _prevPos = transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            if (_legIKDatas == null || _legIKDatas.Length == 0 || _radius <= 0)
                return;

            foreach (var data in _legIKDatas)
            {
                Gizmos.DrawWireSphere(data.LegIKTarget.position, _radius);
            }
        }

        private void InitLegs()
        {
            _legIKs = new LegIKController[_legIKDatas.Length];
            _raycaster = new IkRaycaster(_targetLayer);

            for (int i = 0; i < _legIKs.Length; i++)
            {
                LegIKDistanceTracker iKLegDistanceTracker = new LegIKDistanceTracker(transform, _legIKDatas[i].LegIKTarget, _radius, _legIKDatas[i].Timing);
                LegIKRaycastController legIKRaycastController = new LegIKRaycastController(transform, _legIKDatas[i].LegIKTarget, _raycaster, _stepRadius);

                LegIkMover legIkMover = new LegIkMover(this, _legIKDatas[i].LegIKTarget, legIKRaycastController, _moveCurve, _stepYCurve, _stepDuration, _yMod, _moveThreshold);

                _legIKs[i] = new LegIKController(legIkMover, iKLegDistanceTracker, legIKRaycastController);
            }
        }

        private void Update()
        {
            CheckMovement();

            float deltaTime = Time.deltaTime;

            for (int i = 0; i < _legIKs.Length; i++)
            {
                _legIKs[i].Tick(deltaTime);
            }

            _raycaster.ExecuteRaycasts();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _legIKs.Length; i++)
            {
                _legIKs[i].PhysicTick();
            }
        }

        private void CheckMovement()
        {
            float moveDistance = (_prevPos - transform.position).magnitude;
            _prevPos = transform.position;

            if (_isMoving && moveDistance == 0)
                EndMoving();
            else if (!_isMoving && moveDistance > 0)
                StartMoving();
        }

        private void StartMoving()
        {
            _isMoving = true;
            for (int i = 0; i < _legIKs.Length; i++)
            {
                _legIKs[i].StartMoving();
            }
        }

        private void EndMoving()
        {
            _isMoving = false;
            for (int i = 0; i < _legIKs.Length; i++)
            {
                _legIKs[i].EndMoving();
            }
        }
    }

    [Serializable]
    public struct LegIKData
    {
        public Transform LegIKTarget;
        public float Timing;
    }
}

