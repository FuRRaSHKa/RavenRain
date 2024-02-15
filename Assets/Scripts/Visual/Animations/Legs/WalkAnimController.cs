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
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _moveThreshold;

        [Header("Step Setttings")]
        [SerializeField] private float _stepDuration;
        [SerializeField] private AnimationCurve _stepYCurve;
        [SerializeField] private AnimationCurve _moveCurve;
        [SerializeField] private float _yMod;

        [Header("Legs Setttings")]
        [SerializeField] private float _updateTiming;
        [SerializeField] private LegIKData[] _legIKDatas;

        private LegIKController[] _legIKs;
        private IkRaycaster _raycaster;

        private void Awake()
        {
            InitLegs();
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
                LegIkMover legIkMover = new LegIkMover(this, _legIKDatas[i].LegIKTarget, _moveCurve, _stepYCurve, _stepDuration, _yMod, _moveThreshold);
                LegIKTimer legIKTimer = new LegIKTimer(_legIKDatas[i].Timing, _updateTiming);
                LegIKRaycastController legIKRaycastController = new LegIKRaycastController(transform, _legIKDatas[i].LegIKTarget, _raycaster, _radius);

                _legIKs[i] = new LegIKController(legIkMover, legIKTimer, legIKRaycastController);
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < _legIKs.Length; i++)
            {
                _legIKs[i].Tick(deltaTime);
            }

            _raycaster.ExecuteRaycasts();
        }
    }

    [Serializable]
    public struct LegIKData
    {
        public Transform LegIKTarget;
        public float Timing;
    }
}

