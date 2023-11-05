using HalloGames.RavensRain.Gameplay.Characters;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public abstract class BasePerk : IPerk
    {
        protected readonly CharacterEntity characterEntity;

        protected int stackCount = 1;

        public BasePerk(CharacterEntity characterEntity)
        {
            this.characterEntity = characterEntity;
        }

        public abstract void Apply();

        public abstract void Remove();

        public abstract void Stack();
    }

    public interface IPerk : IApplyable, IStackable, IRemovable
    {

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

