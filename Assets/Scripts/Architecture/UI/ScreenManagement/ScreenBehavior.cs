using UnityEngine;

namespace HalloGames.Architecture.UI.ScreenManagement
{
    public abstract class ScreenBehavior : MonoBehaviour, IScreen
    {
        public bool IsOpened
        {
            get; private set;
        }

        public virtual void Close()
        {
            IsOpened = false;
        }

        public virtual void Open()
        {
            IsOpened = true;
        }
    }

}

