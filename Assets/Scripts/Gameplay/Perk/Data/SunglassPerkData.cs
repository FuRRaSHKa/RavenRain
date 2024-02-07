
using HalloGames.Architecture.CoroutineManagement;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Characters.Stats;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk.Data
{
    [CreateAssetMenu(fileName = "SunglassPerkData", menuName = "Data/Perks/SunglassPerkData")]
    public class SunglassPerkData : PerkData
    {
        [SerializeField] private float _value = 2;

        public override IPerk GetPerk(CharacterEntity characterEntity)
        {
            return new SunglassPerk(characterEntity, PerkDescription, _value, name);
        }
    }
}

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public class SunglassPerk : BasePerk
    {
        private readonly SunglassesMod _statModifyer;
        private readonly string _name;
        private readonly float _modValue;
        private readonly StatTypesEnum _type;

        private int _stackCount = 1;
        private IStopable _stopable;

        public SunglassPerk(CharacterEntity characterEntity, Data.DescriptionStruct description, float value, string name) : base(characterEntity, description)
        {
            _name = name;
            _modValue = value;
            _type = StatTypesEnum.Damage;
            _statModifyer = new SunglassesMod();

            characterEntity.CharacterHealth.OnDamage += DisableBaff;
        }

        private void DisableBaff()
        {
            _statModifyer.SetCurValue(1);
            CharacterEntity.CharacterDataWrapper.UpdateModValue(_type);

            _stopable?.Stop();
            _stopable = RoutineManager.CreateRoutine(CharacterEntity).Wait(3f, SetValue).Start();
        }

        public override void Apply()
        {
            CharacterEntity.CharacterDataWrapper.AddMod(_type, _name, _statModifyer);
            SetValue();
        }

        public override void Remove()
        {
            CharacterEntity.CharacterDataWrapper.RemoveMod(_type, _name);
        }

        private void SetValue()
        {
            _statModifyer.SetCurValue(_modValue * _stackCount);
            CharacterEntity.CharacterDataWrapper.UpdateModValue(_type);
        }

        public override void Stack()
        {
            _stackCount++;
            SetValue();
        }
    }
}

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public class SunglassesMod : IStatModifyer
    {
        private float _value = 1;

        public void SetCurValue(float value)
        {
            _value = value;
        }

        public float ModifyStat(float input)
        {
            return input * _value;
        }
    }
}


