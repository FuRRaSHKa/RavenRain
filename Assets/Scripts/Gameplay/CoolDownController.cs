using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Utils
{
    public class CoolDownController
    {
        private float _targetTime;
        private float _time;

        public void SetTime(float targetTime)
        {
            _targetTime = targetTime;
        }

        public void Update()
        {
            if (_targetTime > _time)
            {
                _time += Time.deltaTime;
                return;
            }
        }

        public void StartCooldown()
        {
            _time = 0;
        }

        public bool IsCooldownEnded()
        {
            return _time >= _targetTime;
        }
    }
}
