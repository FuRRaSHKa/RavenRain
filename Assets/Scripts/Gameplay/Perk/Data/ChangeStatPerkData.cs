using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Perk.Logic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk.Data
{
    [CreateAssetMenu(fileName = "ChangeStatPerk", menuName = "Data/Perks/ChangeStatPerk")]
    public class ChangeStatPerkData : PerkData
    {
        [SerializeField] private float _value;
        [SerializeField] private ModifyType _modifyType;
        [SerializeField] private StatTypesEnum _statTypes;

        public override IPerk GetPerk(CharacterEntity characterEntity)
        {
            return new ChangeStatPerk(characterEntity, _modifyType, name, _value, _statTypes);
        }
    }

}

