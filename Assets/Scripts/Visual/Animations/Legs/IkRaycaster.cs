using HalloGames.Architecture.CoroutineManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace HalloGames.RavensRain.Visuals.Animations.IK.Raycasts
{
    public class IkRaycaster : IRaycastable
    {
        private readonly LayerMask _targetLayer;

        private List<IRaycastRequest> _requests;

        private IStopable _raycastExecutionProcces;

        public IkRaycaster(LayerMask targetLayer)
        {
            _requests = new List<IRaycastRequest>();
            _targetLayer = targetLayer;
        }

        public void Raycast(IRaycastRequest raycastRequest)
        {
            _requests.Add(raycastRequest);
        }

        public void ExecuteRaycasts()
        {
            if (_raycastExecutionProcces != null && _raycastExecutionProcces.IsRunning)
                return;

            if (_requests.Count == 0)
                return;

            _raycastExecutionProcces = RoutineManager.CreateRoutine(ExecuteRaycastsCoroutine()).Start();
        }

        private IEnumerator ExecuteRaycastsCoroutine()
        {
            NativeArray<RaycastHit> results = new NativeArray<RaycastHit>(_requests.Count, Allocator.TempJob);
            NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(_requests.Count, Allocator.TempJob);

            for (int i = 0; i < _requests.Count; i++)
            {
                RaycastData raycastData = _requests[i].GetRaycastData();
                commands[i] = new RaycastCommand(raycastData.Origin, raycastData.Direction, new QueryParameters(_targetLayer.value), raycastData.Distance);
            }

            JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 4);
            yield return null;

            handle.Complete();

            for (int i = 0; i < results.Length; i++)
            {
                _requests[i].Complete(results[i].collider != null, results[i].point);
            }

            _requests.RemoveRange(0, _requests.Count);

            commands.Dispose();
            results.Dispose();
        }
    }

    public interface IRaycastable
    {
        public void Raycast(IRaycastRequest raycastRequest);
    }

    public interface IRaycastRequest
    {
        public RaycastData GetRaycastData();
        public void Complete(bool result, Vector3 pos);
    }

    public class RaycastRequest : IRaycastRequest
    {
        private readonly RaycastData _raycastData;

        private Action<bool, Vector3> _callback;

        public RaycastRequest(RaycastData raycastData, Action<bool, Vector3> callback)
        {
            _raycastData = raycastData;
            _callback = callback;
        }

        public void Complete(bool result, Vector3 pos)
        {
            _callback?.Invoke(result, pos);
        }

        public RaycastData GetRaycastData()
        {
            return _raycastData; 
        }
    }

    public struct RaycastData
    {
        public readonly Vector3 Origin;
        public readonly Vector3 Direction;
        public readonly float Distance;

        public RaycastData(Vector3 origin, Vector3 direction, float distance)
        {
            Origin = origin;
            Direction = direction;
            Distance = distance;
        }
    }
}


