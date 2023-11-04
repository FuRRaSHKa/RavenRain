using HalloGames.RavensRain.Management.Input;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Movement
{
    public class PlayerRotator : MonoBehaviour
    {
        private IValueInput _inputService;

        public void InitInput(IValueInput inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 worldMousePos = _inputService.PlayerMouseWorldPos;
            Vector3 direction = worldMousePos - transform.position;
            direction.y = 0;

            if (direction == Vector3.zero)
                return;

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }

}

