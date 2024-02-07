using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using UnityEngine;
using UnityEngine.Windows;

namespace HalloGames.RavensRain.Gameplay.Perk.Logic
{
    public class ChangeStatPerk : BasePerk
    {
        private readonly StatModifyer _statModifyer;
        private readonly float _perStackValue;
        private readonly string _name;
        private readonly float _modValue;
        private readonly StatTypesEnum _type;

        public ChangeStatPerk(CharacterEntity characterEntity, DescriptionStruct description, ModifyType modifyType, string name, float modValue, StatTypesEnum type, float perStackValue) : base(characterEntity, description)
        {
            _perStackValue = perStackValue;
            _modValue = modValue;
            _name = name;
            _type = type;
            _statModifyer = new StatModifyer(modifyType, _modValue);
        }

        public override void Apply()
        {
            CharacterEntity.CharacterDataWrapper.AddMod(_type, _name, _statModifyer);
        }

        public override void Remove()
        {
            CharacterEntity.CharacterDataWrapper.RemoveMod(_type, _name);
        }

        public override void Stack()
        {
            SetStackCount(1 + StackCount);
            float value = _modValue + StackCount * _perStackValue;

            _statModifyer.SetValue(value);
            CharacterEntity.CharacterDataWrapper.UpdateModValue(_type);
        }
    }

}

