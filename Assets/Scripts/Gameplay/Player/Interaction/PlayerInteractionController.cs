using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.Utils.Layer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Player.Interaction
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _characterEntity;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private string _perkTag;

        private List<IPickable> _pickablesInRadius = new List<IPickable>();

        public void PickUp()
        {
            if (_pickablesInRadius.Count == 0)
                return;

            IPickable pickable = _pickablesInRadius.Last();
            pickable.PickUp(_characterEntity, out bool needToRemove);
            if (needToRemove)
                _pickablesInRadius.Remove(pickable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_targetLayer.Contains(other.gameObject.layer))
                return;

            if (other.CompareTag(_perkTag) && other.TryGetComponent(out IPickable pickable))
                _pickablesInRadius.Add(pickable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_targetLayer.Contains(other.gameObject.layer))
                return;

            if (other.CompareTag(_perkTag) && other.TryGetComponent(out IPickable pickable))
                _pickablesInRadius.Remove(pickable);
        }
    }

    public interface IPickable
    {
        public void PickUp(CharacterEntity characterEntity, out bool needToRemove);
    }
}

