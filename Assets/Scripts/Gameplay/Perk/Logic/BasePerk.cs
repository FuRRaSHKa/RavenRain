using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public abstract class BasePerk : IPerk
    {
        private readonly DescriptionStruct _description;

        private readonly CharacterEntity _characterEntity;

        private int _stackCount = 1;

        protected CharacterEntity CharacterEntity => _characterEntity;
        protected int StackCount => _stackCount;

        public BasePerk(CharacterEntity characterEntity, DescriptionStruct description)
        {
            _description = description; 

            _characterEntity = characterEntity;
        }

        public DescriptionStruct PerkDescription => _description;

        public abstract void Apply();

        public abstract void Remove();

        public abstract void Stack();

        protected void SetStackCount(int value)
        {
            _stackCount = value;
        }
    }

    public interface IPerk : IApplyable, IStackable, IRemovable
    {
        public DescriptionStruct PerkDescription 
        {
            get;
        }
    }

    public interface IApplyable
    {
        public void Apply();
    }

    public interface IStackable
    {
        public void Stack();
    }

    public interface IRemovable
    {
        public void Remove();
    }
}

