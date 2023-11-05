using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HalloGames.RavensRain.UI.Perks
{
    public class PerkLayout : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _parent;
        [SerializeField] private PerkLayoutElement _prefab;
        [SerializeField] private CharacterPerkHandler _playerPerks;
        [SerializeField] private int _maxCountWithoutResize;
        [SerializeField] private Vector2 _smallSize;

        private Dictionary<string, PerkLayoutElement> _perks = new Dictionary<string, PerkLayoutElement>();

        private void Awake()
        {
            _playerPerks.OnPerkAdd += SpawnPerk;
        }

        private void SpawnPerk(DescriptionStruct perkDescriptionStruct)
        {
            if (_perks.ContainsKey(perkDescriptionStruct.Name))
            {
                _perks[perkDescriptionStruct.Name].Stack();
                return;
            }

            PerkLayoutElement perkLayoutElement = Instantiate(_prefab, _parent.transform);
            perkLayoutElement.Init(perkDescriptionStruct);
            _perks.Add(perkDescriptionStruct.Name, perkLayoutElement);

            if (_perks.Count > _maxCountWithoutResize)
                _parent.cellSize = _smallSize;
        }
    }
}


