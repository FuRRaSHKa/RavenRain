using HalloGames.RavensRain.Gameplay.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk.Data
{
    public abstract class PerkData : ScriptableObject
    {
        [SerializeField] private DescriptionStruct _perkDescription;

        public DescriptionStruct PerkDescription => _perkDescription;

        public abstract IPerk GetPerk(CharacterEntity characterEntity);
    }

    [Serializable]
    public struct DescriptionStruct
    {
        public string Name;
        public string ShortDescription;
        public string LoreDescription;
        public Sprite Sprite;
        public Color Color;
    }
}

