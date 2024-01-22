using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        [SerializeField, Tooltip("Time in seconds")] private float timerTime;
        private float currentTime;
        private int seconds;
        private bool timeOver;

        public UnityEvent TimeOverEvent;

        private void Start()
        {
            currentTime = timerTime;
        }

        private void Update()
        {
            if(!timeOver) DecreaseTime();
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

        public void ResetTime()
        {
            currentTime = timerTime;
        }
    }

}