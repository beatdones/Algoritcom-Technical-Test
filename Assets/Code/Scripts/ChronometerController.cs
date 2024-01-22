using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Algoritcom.TechnicalTest.Chronometer
{
    public class ChronometerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text chronometerText;

        private float timeElapsed;
        private int minutes, seconds, cents;

        private static bool gameIsStarted;

        private void Update()
        {
            if(gameIsStarted) IncreaseTime();
        }

        private void IncreaseTime()
        {
            timeElapsed += Time.deltaTime;

            minutes = (int)(timeElapsed / 60f);
            seconds = (int)(timeElapsed - minutes * 60f);
            cents = (int)((timeElapsed - (int)timeElapsed) * 100f);

            chronometerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
        }

        public void ResetTime()
        {
            timeElapsed = 0f;
        }

        public void SetGameIsStarted(bool value)
        {
            gameIsStarted = value;
        }
    }

}
