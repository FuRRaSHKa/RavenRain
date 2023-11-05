using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public abstract class BasePerk : IPerk
    {
        private readonly DescriptionStruct _description;

        protected readonly CharacterEntity characterEntity;

        protected int stackCount = 1;

        public BasePerk(CharacterEntity characterEntity, DescriptionStruct description)
        {
            _description = description; 

            this.characterEntity = characterEntity;
        }

        public DescriptionStruct PerkDescription => _description;

        public abstract void Apply();

        public abstract void Remove();

        public abstract void Stack();
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

