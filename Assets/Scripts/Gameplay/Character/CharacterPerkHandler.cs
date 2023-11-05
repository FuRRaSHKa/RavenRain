using HalloGames.RavensRain.Gameplay.Perk;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    public class CharacterPerkHandler : MonoBehaviour
    {
        private Dictionary<string, IPerk> _perks = new Dictionary<string, IPerk>();

        public event Action<DescriptionStruct> OnPerkAdd;

        public void AddPerk(string name, IPerk perk)
        {
            if (_perks.ContainsKey(name))
            {
                _perks[name].Stack();
            }          
            else
            {
                _perks.Add(name, perk);
                perk.Apply();
            }

            OnPerkAdd?.Invoke(perk.PerkDescription);
        }

        public void RemovePerk(string name)
        {
            if (_perks.ContainsKey(name))
            {
                _perks[name].Remove();
                _perks.Remove(name);
            } 
        }
    }
}


