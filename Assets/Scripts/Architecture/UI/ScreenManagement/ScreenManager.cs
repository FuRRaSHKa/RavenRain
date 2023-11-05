using System;
using System.Collections.Generic;
using System.Linq;
using HalloGames.Architecture.Singletones;
using UnityEngine;

namespace HalloGames.Architecture.UI.ScreenManagement
{

    public class ScreenManager : MonoSingleton<ScreenManager>
    {
        [SerializeField] private List<ScreenBehavior> _screens;

        private Stack<ScreenBehavior> _screensStack = new Stack<ScreenBehavior>();

        public static event Action<ScreenBehavior> OnScreenOpened;
        public static event Action<ScreenBehavior> OnScreenClosed;

        public ScreenBehavior CurrentScreen
        {
            get
            {
                if (_screensStack.TryPeek(out ScreenBehavior result))
                    return result;
                return null;
            }
        }

        public TScreen OpenScreenAdditive<TScreen>() where TScreen : ScreenBehavior
        {
            TScreen screen = GetScreen<TScreen>();
            if (!screen)
                return null;

            _screensStack.Push(screen);
            OpenScreenInternal(screen);
            return screen;
        }

        public TScreen OpenScreen<TScreen>() where TScreen : ScreenBehavior
        {
            TScreen screen = GetScreen<TScreen>();
            if (!screen)
                return null;

            if (CurrentScreen == screen)
                return screen;

            CloseAllScreens();
            _screensStack.Push(screen);
            OpenScreenInternal(screen);
            return screen;
        }

        public void CloseAdditiveScreens()
        {
            if (_screensStack.Count <= 1)
                return;

            ScreenBehavior screen = _screensStack.Pop();
            CloseScreenInternal(screen);
            OpenNextScreen();
        }

        public void CloseOpenedScreen()
        {
            if (_screensStack.Count == 0)
                return;

            ScreenBehavior screen = _screensStack.Pop();
            CloseScreenInternal(screen);
            OpenNextScreen();
        }

        public void OpenNextScreen()
        {
            if (_screensStack.Count == 0)
                return;

            ScreenBehavior screen = _screensStack.Peek();
            OpenScreenInternal(screen);
        }

        public void CloseAllScreens()
        {
            int count = _screensStack.Count;
            for (int i = 0; i < count; i++)
            {
                ScreenBehavior screen = _screensStack.Pop();
                CloseScreenInternal(screen);
            }
        }

        public void AddScreen(ScreenBehavior screen)
        {
            Instance._screens.Add(screen);
        }

        public TScreen GetScreen<TScreen>() where TScreen : ScreenBehavior
        {
            Type type = typeof(TScreen);
            ScreenBehavior screen = Instance._screens.DefaultIfEmpty(null).FirstOrDefault(f => f.GetType() == type);
            return screen as TScreen;
        }

        public bool IsScreenOpened<TScreen>() where TScreen : ScreenBehavior
        {
            TScreen screen = GetScreen<TScreen>();
            return screen && screen.IsOpened;
        }

        private void OpenScreenInternal(ScreenBehavior screen)
        {
            screen.Open();
            OnScreenOpened?.Invoke(screen);
        }

        private void CloseScreenInternal(ScreenBehavior screen)
        {
            screen.Close();
            OnScreenClosed?.Invoke(screen);
        }
    }

}
