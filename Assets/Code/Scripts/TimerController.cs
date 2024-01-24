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
        [SerializeField] private TMP_Text timerText;

        [SerializeField, Tooltip("Time in seconds")] private float timerTime;
        private float currentTime;
        private int seconds;
        private bool timeOver;

        public UnityEvent TimeOverEvent;

        private static bool gameIsStarted;

        private void Start()
        {
            currentTime = timerTime;

            SwishDetector.OnTriggerEnterEvent += ResetTime;
        }

        private void Update()
        {
            if(!timeOver && gameIsStarted) DecreaseTime();
        }

        private void DecreaseTime()
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0) currentTime = 0;

            timerText.text = string.Format("{0:00}", currentTime);

            if (currentTime == 0) TimeOver();
        }

        private void TimeOver()
        {
            TimeOverEvent.Invoke();
            timeOver = true;
        }

        private void ResetTime()
        {
            currentTime = timerTime;
        }

        public void SetGameIsStarted(bool value)
        {
            gameIsStarted = value;
        }
    }

}
