using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    public class CharacterEntity : MonoBehaviour
    {
        [SerializeField] private WeaponDataWrapper _weapon;
        [SerializeField] private CharacterDataWrapper _characterDataWrapper;
        [SerializeField] private CharacterPerkHandler _perkHandler;
        [SerializeField] private CharacterHealth _characterHealth;

        public CharacterDataWrapper CharacterDataWrapper => _characterDataWrapper;
        public WeaponDataWrapper WeaponDataWrapper => _weapon;
        public CharacterPerkHandler PerkHandler => _perkHandler;
        public CharacterHealth CharacterHealth => _characterHealth;
    }
}

