using HalloGames.Architecture.CoroutineManagement;
using HalloGames.RavensRain.Visuals.Animations.IK.Raycasts;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Visuals.Animations.IK
{
    public class LegIKController
    {
        private readonly LegIKTimer _iKTimer;
        private readonly LegIkMover _ikMover;
        private readonly LegIKRaycastController _ikRaycastController;

        public LegIKController(LegIkMover legIkMover, LegIKTimer iKTimer, LegIKRaycastController legIKRaycastController)
        {
            _iKTimer = iKTimer;
            _ikMover = legIkMover;
            _ikRaycastController = legIKRaycastController;

            Subscribe();
        }

        private void Subscribe()
        {
            _iKTimer.OnTiming += RaycastLeg;
        }

        public void Tick(float deltaTime)
        {
            _ikRaycastController.Tick(deltaTime);
            _iKTimer.Tick(deltaTime);
        }

        public void Reset()
        {
            _iKTimer.ResetTiming();
        }

        private void RaycastLeg()
        {
            _ikRaycastController.Raycast(MoveLeg);
        }

        private void MoveLeg(bool result, Vector3 targetPos)
        {
            if (!result)
                return;

            _ikMover.MoveLeg(targetPos);
        }
    }

    public class LegIKRaycastController
    {
        private readonly Transform _parent;
        private readonly IRaycastable _ikRaycaster;

        private readonly float _legRadius;

        private Vector3 _prevParentPos;
        private Vector3 _targetRaycastOrigin;
        private Vector3 _targetRaycastDirection;
        private Vector3 _originLocalPos;
        private Vector3 _moveDirection;

        private float _raycastDistance;

        public LegIKRaycastController(Transform parent, Transform ikTarget, IRaycastable raycastable, float legRadius)
        {
            _parent = parent;
            _legRadius = legRadius;
            _ikRaycaster = raycastable;

            BakePoses(ikTarget);
        }

        private void BakePoses(Transform ikTarget)
        {
            _prevParentPos = _parent.position;

            _originLocalPos = ikTarget.position - _prevParentPos;
        }

        private void CreateRaycastPos()
        {
            Vector3 targetRaycastPos = _moveDirection + _parent.rotation * _originLocalPos + _parent.position;
            _targetRaycastOrigin = targetRaycastPos + _parent.up;
            _targetRaycastDirection = (targetRaycastPos - _targetRaycastOrigin);

            _raycastDistance = _targetRaycastDirection.magnitude + _legRadius;
        }

        public void Raycast(Action<bool, Vector3> callback)
        {
            CreateRaycastPos();

            RaycastData data = new RaycastData(_targetRaycastOrigin, _targetRaycastDirection, _raycastDistance);
            RaycastRequest raycastRequest = new RaycastRequest(data, callback);

            _ikRaycaster.Raycast(raycastRequest);
        }

        public void Tick(float deltaTime)
        {
            CheckMove();
        }

        private void CheckMove()
        {
            Vector3 pos = _parent.position;

            _moveDirection = pos - _prevParentPos;
            _prevParentPos = pos;

            Vector3 upMask = _parent.up;
            _moveDirection -= Vector3.Scale(_moveDirection, upMask);
            _moveDirection = _moveDirection.normalized * _legRadius;
        }
    }

    public class LegIKTimer
    {
        private readonly float _eventTiming;
        private readonly float _resetTiming;

        private float _currentTiming;

        private bool _eventSended;

        public event Action OnTiming;

        public LegIKTimer(float eventTiming, float resetTiming)
        {
            _eventTiming = eventTiming;
            _resetTiming = resetTiming;

            _eventSended = false;
            _currentTiming = 0;
        }

        public void ResetTiming()
        {
            _currentTiming = 0;
            _eventSended = false;
        }

        public void Tick(float deltaTime)
        {
            _currentTiming += deltaTime;

            if (_currentTiming >= _eventTiming && !_eventSended)
            {
                _eventSended = true;
                OnTiming?.Invoke();
            }

            if (_currentTiming >= _resetTiming)
                ResetTiming();
        }
    }

    public class LegIkMover
    {
        private readonly MonoBehaviour _parent;
        private readonly Transform _legIkTarget;
        private readonly AnimationCurve _moveCurve;
        private readonly AnimationCurve _yCurve;

        private readonly float _yMod;
        private readonly float _moveTime;
        private readonly float _moveThreshold;

        private Vector3 _startPosition;
        private Vector3 _targetPos;

        private IStopable _moveRoutine;

        public LegIkMover(MonoBehaviour parent, Transform legIKTarget, AnimationCurve moveCurve, AnimationCurve yCurve, float moveTime, float yMod, float moveThreshold)
        {
            _legIkTarget = legIKTarget;
            _moveCurve = moveCurve;
            _yCurve = yCurve;
            _moveTime = moveTime;
            _parent = parent;
            _yMod = yMod;
            _moveThreshold = moveThreshold;
        }

        public void MoveLeg(Vector3 targetPos)
        {
            if (targetPos == _targetPos || (targetPos - _targetPos).magnitude < _moveThreshold)
                return;

            _startPosition = _legIkTarget.position;
            _targetPos = targetPos;

            _moveRoutine?.Stop();
            _moveRoutine = RoutineManager.CreateRoutine(_parent).AnimateValue(_startPosition, _targetPos, SetPos, LerpWithCurves, _moveTime).Start();

            Vector3 LerpWithCurves(Vector3 start, Vector3 end, float value)
            {
                return Vector3.Lerp(start, end, _moveCurve.Evaluate(value)) + new Vector3(0, _yCurve.Evaluate(value) * _yMod, 0);
            }

            void SetPos(Vector3 pos)
            {
                _legIkTarget.position = pos;
            }
        }
    }
}
