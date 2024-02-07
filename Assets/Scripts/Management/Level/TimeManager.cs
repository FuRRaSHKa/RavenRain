using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Level
{
    public class TimeManager : MonoBehaviour
    {
       
        [SerializeField] private TMP_Text _time;

        private float _dayTime;

        private void Update()
        {
            _dayTime += Time.deltaTime;
            _time.text = ((int)_dayTime).ToString();
        }
    }

}

