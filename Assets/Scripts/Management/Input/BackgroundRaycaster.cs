using UnityEngine;
using UnityEngine.InputSystem;
using HalloGames.Architecture.Singletones;

namespace HalloGames.RavensRain.Management.Input
{
    public class BackgroundRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _backgroundLayer;
        [SerializeField] private float _distance;

        private Camera _camera;
        private Vector3 _pos;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 rawPos = Mouse.current.position.ReadValue();
            rawPos.z = 5;
            Ray ray = _camera.ScreenPointToRay(rawPos);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, _distance, _backgroundLayer))
                _pos = raycastHit.point;
        }

        public Vector3 GetWorldMousePos()
        {
            return _pos;
        }
    }
}