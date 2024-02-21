using System;
using UnityEngine;

namespace HalloGames.RavensRain.Visuals.Animations.IK
{

    public interface ILegNotifyer
    {
        public void Subscribe(Action callback);

        public void Tick(float deltaTime);

        public void End();

        public void Start();
    }

    public class LegIKTimer : ILegNotifyer
    {
        private readonly float _eventTiming;
        private readonly float _resetTiming;

        private float _currentTiming;

        private bool _eventSended;

        private Action _onTiming;

        public LegIKTimer(float eventTiming, float resetTiming)
        {
            _eventTiming = eventTiming;
            _resetTiming = resetTiming;

            _eventSended = false;
            _currentTiming = 0;
        }

        private void Reset()
        {
            _currentTiming = 0;
            _eventSended = false;
        }

        public void Start()
        {
            Reset();
        }

        public void Subscribe(Action callback)
        {
            _onTiming += callback;
        }

        public void Tick(float deltaTime)
        {
            _currentTiming += deltaTime;

            if (_currentTiming >= _eventTiming && !_eventSended)
            {
                _eventSended = true;
                _onTiming?.Invoke();
            }

            if (_currentTiming >= _resetTiming)
                Reset();
        }

        public void End()
        {
            _currentTiming = _eventTiming;
            _eventSended = false;
        }
    }

    public class LegIKDistanceTracker : ILegNotifyer
    {
        private readonly Transform _parent;
        private readonly Transform _ikTarget;

        private readonly float _startDelay;
        private readonly float _maxDistance;
        private readonly Vector3 _localCenter;

        private Action _onOverextend;

        private float _curDelay;
        private bool _invokeOnEndDelay; 

        public LegIKDistanceTracker(Transform parent, Transform ikTarget, float maxDistance, float startDelay)
        {
            _parent = parent;
            _ikTarget = ikTarget;
            _maxDistance = maxDistance;
            _startDelay = startDelay;

            _invokeOnEndDelay = false;
            _curDelay = 0;

            _localCenter = _ikTarget.position - _parent.position;
        }

        public void End()
        {
            _invokeOnEndDelay = true;
            if (_curDelay <= 0)
                _curDelay = _startDelay;
        }

        public void Start()
        {
            _invokeOnEndDelay = true;
            _curDelay = _startDelay;
        }

        public void Subscribe(Action callback)
        {
            _onOverextend += callback;
        }

        public void Tick(float deltaTime)
        {
            if (_curDelay > 0)
            {
                _curDelay -= deltaTime;
            }
            else if (_invokeOnEndDelay)
            {
                _invokeOnEndDelay = false;
                _onOverextend.Invoke();
                return;
            }

            Vector3 delta = _parent.position + _localCenter - _ikTarget.position;
            delta.y = 0;

            if (delta.magnitude <= _maxDistance)
                return;

            _invokeOnEndDelay = false;
            _onOverextend.Invoke();
        }
    }
}