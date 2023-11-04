using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public abstract class BasePerk : IPerk
    {
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

