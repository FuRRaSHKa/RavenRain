using HalloGames.RavensRain.Gameplay.Perk;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters
{
    public class CharacterPerkHandler : MonoBehaviour
    {
        private Dictionary<string, IPerk> _perks = new Dictionary<string, IPerk>();

        public void AddPerk(string name, IPerk perk)
        {
            if (_perks.ContainsKey(name))
            {
                _perks[name].Stack();
                _perks[name].Apply();
            }          
            else
            {
                _perks.Add(name, perk);
                perk.Apply();
            }    
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


