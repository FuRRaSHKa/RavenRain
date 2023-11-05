

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public class StatModifyer : IStatModifyer
    {
        private readonly ModifyType _modifyType;

        private float _modifyValue;

        public StatModifyer(ModifyType modifyType, float modifyValue)
        {
            _modifyType = modifyType;
            _modifyValue = modifyValue;
        }

        public float ModifyStat(float input)
        {
            switch(_modifyType )
            {
                case ModifyType.Additive:
                    return _modifyValue + input;

                case ModifyType.Multiplicative: 
                    return _modifyValue * input;
            }

            return input;
        }

        public void SetValue(float value)
        {
            _modifyValue = value;
        }
    }

    public interface IStatModifyer
    {
        public float ModifyStat(float input);
    }

    public enum ModifyType
    {
        Additive,
        Multiplicative
    }

}

