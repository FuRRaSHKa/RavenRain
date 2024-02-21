using HalloGames.Architecture.CoroutineManagement;
using HalloGames.RavensRain.Visuals.Animations.IK.Raycasts;
using System;
using UnityEngine;

namespace HalloGames.RavensRain.Visuals.Animations.IK
{
    public class LegIKController
    {
        private readonly ILegNotifyer _legNotifyer;
        private readonly LegIkMover _ikMover;
        private readonly LegIKRaycastController _ikRaycastController;

        public LegIKController(LegIkMover legIkMover, ILegNotifyer legNotifyer, LegIKRaycastController legIKRaycastController)
        {
            _legNotifyer = legNotifyer;
            _ikMover = legIkMover;
            _ikRaycastController = legIKRaycastController;

            Subscribe();
        }

        private void Subscribe()
        {
            _legNotifyer.Subscribe(RaycastLeg);
        }

        public void Tick(float deltaTime)
        {
            _ikRaycastController.Tick(deltaTime);
            _legNotifyer.Tick(deltaTime);
            _ikMover.Tick(deltaTime);
        }

        public void PhysicTick()
        {
            _ikMover.PhysicTick();
        }

        public void Start()
        {
            _legNotifyer.Start();
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

        public void EndMoving()
        {
            _legNotifyer.End();
        }

        public void StartMoving()
        {
            _legNotifyer.Start();
        }
    }

    public class LegIKRaycastController
    {
        private readonly Transform _parent;
        private readonly IRaycastable _ikRaycaster;

        private readonly float _legStepRadius;

        private Vector3 _prevParentPos;
        private Vector3 _targetRaycastOrigin;
        private Vector3 _targetRaycastDirection;
        private Vector3 _originLocalPos;
        private Vector3 _moveDirection;

        private float _raycastDistance;

        public LegIKRaycastController(Transform parent, Transform ikTarget, IRaycastable raycastable, float legStepRadius)
        {
            _parent = parent;
            _legStepRadius = legStepRadius;
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

            _raycastDistance = _targetRaycastDirection.magnitude + _legStepRadius;
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
            _moveDirection = _moveDirection.normalized * _legStepRadius;
        }
    }

    public class LegIkMover
    {
        private readonly MonoBehaviour _parent;
        private readonly Transform _legIkTarget;
        private readonly AnimationCurve _moveCurve;
        private readonly AnimationCurve _yCurve;
        private readonly LegIKRaycastController _legIKRaycastController;

        private readonly float _yMod;
        private readonly float _moveTime;
        private readonly float _moveThreshold;

        private Vector3 _startPosition;
        private Vector3 _currentPos;
        private Vector3 _targetPos;

        private float _currentTime;

        private bool _isMoving = false;

        public LegIkMover(MonoBehaviour parent, Transform legIKTarget, LegIKRaycastController legIKRaycastController, AnimationCurve moveCurve, AnimationCurve yCurve, float moveTime, float yMod, float moveThreshold)
        {
            _legIKRaycastController = legIKRaycastController;
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
            if (targetPos == _currentPos || (targetPos - _currentPos).magnitude < _moveThreshold)
                return;

            _startPosition = _legIkTarget.position - _parent.transform.position;
            _currentPos = _startPosition;
            _targetPos = targetPos - _parent.transform.position;

            _isMoving = true;
            _currentTime = 0;
        }

        private Vector3 LerpPos(Vector3 start, Vector3 end, float value)
        {
            return Vector3.Lerp(start, end, _moveCurve.Evaluate(value)) + new Vector3(0, _yCurve.Evaluate(value) * _yMod, 0);
        }

        private void SetPos(Vector3 pos)
        {
            _currentPos = pos + _parent.transform.position;
            _legIkTarget.position = _currentPos;
        }

        public void Tick(float deltaTime)
        {
            if (!_isMoving)
                return;

            _currentTime += deltaTime;
            if (_currentTime >= _moveTime)
                _isMoving = false;

            SetPos(LerpPos(_startPosition, _targetPos, _currentTime / _moveTime));
        }

        public void PhysicTick()
        {
            if (!_isMoving)
                return;

            CheckTargetPos();
        }

        private void CheckTargetPos()
        {
            _legIKRaycastController.Raycast(UpdateTargetPos);
        }

        private void UpdateTargetPos(bool result, Vector3 targetPos)
        {
            if (!result || !_isMoving)
                return;

            _targetPos = targetPos - _parent.transform.position;
        }
    }
}
