using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    public class CharacterEntity : MonoBehaviour
    {
        [SerializeField] private WeaponDataWrapper _weapon;
        [SerializeField] private CharacterDataWrapper _characterDataWrapper;
        [SerializeField] private CharacterPerkHandler _perkHandler;

        public CharacterDataWrapper CharacterDataWrapper => _characterDataWrapper;
        public WeaponDataWrapper WeaponDataWrapper => _weapon;
        public CharacterPerkHandler PerkHandler => _perkHandler;
    }
}

