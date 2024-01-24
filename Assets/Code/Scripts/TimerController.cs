using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Algoritcom.TechnicalTest.Score;

namespace Algoritcom.TechnicalTest.Timer
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        [SerializeField, Tooltip("Time in seconds")] private float _timerTime;

        private float _currentTime;
        private bool _timeOver;

        private static bool _gameIsStarted;

        public UnityEvent TimeOverEvent;


        private void Start()
        {
            _currentTime = _timerTime;

            SwishDetector.OnTriggerEnterEvent += ResetTime;

        }

        private void Update()
        {
            if(!_timeOver && _gameIsStarted) DecreaseTime();
        }

        private void DecreaseTime()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime < 0) _currentTime = 0;

            _timerText.text = string.Format("{0:00}", _currentTime);

            if (_currentTime == 0) TimeOver();
        }

        private void TimeOver()
        {
            TimeOverEvent.Invoke();
            _timeOver = true;
        }

        private void ResetTime()
        {
            _currentTime = _timerTime;
        }

        public void SetGameIsStarted(bool value)
        {
            _gameIsStarted = value;
        }
    }

}
