using HalloGames.Architecture.CoroutineManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Movement
{
    public class DashController
    {
        private readonly float _dashCooldown;

        private bool _isCooldownEnded = true;
        private IStopable _stopable;

        public DashController(float dashCooldown)
        {
            _dashCooldown = dashCooldown;
        }

        public bool IsAbleToDash()
        {
            return _isCooldownEnded;
        }

        public void DashEnded()
        {
            _isCooldownEnded = false;

            _stopable?.Stop();
            _stopable = RoutineManager.CreateRoutine().Wait(_dashCooldown, CooldownEnded).Start();
        }

        private void CooldownEnded()
        {
            _isCooldownEnded = true;
        }
    }
}