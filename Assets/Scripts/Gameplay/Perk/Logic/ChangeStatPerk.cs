using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk.Logic
{
    public class ChangeStatPerk : BasePerk
    {
        private readonly StatModifyer _statModifyer;
        private readonly string _name;
        private readonly float _modValue;
        protected readonly StatTypesEnum _type; 

        public ChangeStatPerk(CharacterEntity characterEntity, ModifyType modifyType, string name, float modValue, StatTypesEnum type) : base(characterEntity)
        {
            _modValue = modValue;
            _name = name;
            _type = type;
            _statModifyer = new StatModifyer(modifyType, _modValue);
        }

        public override void Apply()
        {
            characterEntity.CharacterDataWrapper.AddMod(_type, _name, _statModifyer);
        }

        public override void Remove()
        {
            characterEntity.CharacterDataWrapper.RemoveMod(_type, _name);
        }

        public override void Stack()
        {
            stackCount++;
            _statModifyer.SetValue(_modValue * stackCount);
        }
    }

}

