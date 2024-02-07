using HalloGames.Architecture.UI.ScreenManagement;
using HalloGames.RavensRain.UI.Joyrnal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HalloGames.RavensRain.UI
{
    public class MainScreen : ScreenBehavior
    {
        [SerializeField] private int _gameplayIndex;

        public void StartGame()
        {
            SceneManager.LoadScene(_gameplayIndex);
        }

        public void OpenJoyrnal()
        {
            ScreenManager.Instance.OpenScreen<JoyrnalScreen>();
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}


