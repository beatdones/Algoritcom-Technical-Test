using TMPro;
using UnityEngine;
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

        // EVENTS
        public delegate void GameOverEvent();
        public static event GameOverEvent OnGameOverEvent;


        #region UNITY METHODS
        private void Start()
        {
            _currentTime = _timerTime;

            SwishDetector.OnTriggerEnterEvent += ResetTime;
        }

        private void Update()
        {
            if(!_timeOver && _gameIsStarted) DecreaseTime();
        }
        #endregion

        #region PRIVATE METHODS
        private void DecreaseTime()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime < 0) _currentTime = 0;

            _timerText.text = string.Format("{0:00}", _currentTime);

            if (_currentTime == 0) TimeOver();
        }

        private void TimeOver()
        {
            OnGameOverEvent?.Invoke();
            _timeOver = true;

            ResetTime();
        }

        private void OnDisable()
        {
            SwishDetector.OnTriggerEnterEvent -= ResetTime;
        }
        #endregion

        #region PUBLIC METHODS
        public void ResetTime()
        {
            _currentTime = _timerTime;
            _timeOver = false;
        }

        public void SetGameIsStarted(bool value)
        {
            _gameIsStarted = value;
        }
        #endregion
    }

}
