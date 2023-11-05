using HalloGames.RavensRain.Gameplay.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk.Data
{
    public abstract class PerkData : ScriptableObject
    {
        public abstract IPerk GetPerk(CharacterEntity characterEntity);
    }
}

