using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Algoritcom.TechnicalTest.Chronometer
{
    public class ChronometerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _chronometerText;

        private float _timeElapsed;
        private int _minutes, _seconds, _cents;

        private static bool _gameIsStarted;

        private void Update()
        {
            if(_gameIsStarted) IncreaseTime();
        }

        private void IncreaseTime()
        {
            _timeElapsed += Time.deltaTime;

            _minutes = (int)(_timeElapsed / 60f);
            _seconds = (int)(_timeElapsed - _minutes * 60f);
            _cents = (int)((_timeElapsed - (int)_timeElapsed) * 100f);

            _chronometerText.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _cents);
        }

        public void ResetTime()
        {
            _timeElapsed = 0f;
        }

        public void SetGameIsStarted(bool value)
        {
            _gameIsStarted = value;
        }
    }

}
