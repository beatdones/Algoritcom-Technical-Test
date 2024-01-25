using UnityEngine;
using Algoritcom.TechnicalTest.Timer;
using Algoritcom.TechnicalTest.Score;
using Algoritcom.TechnicalTest.BallSpawn;
using Algoritcom.TechnicalTest.Character;
using Algoritcom.TechnicalTest.Chronometer;
using Algoritcom.TechnicalTest.ThirPersonController;

namespace Algoritcom.TechnicalTest.GameManagerController
{
    public class GameManager : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private TimerController _timer;
        [SerializeField] private ChronometerController _chronometer;
        [SerializeField] private ScoreController _score;

        private const float _tripleScoringZone = 7.3f;

        private int _amountShootingZone;

        public static bool gameIsPaused = false;

        public delegate void RestartStartGameEvent();
        public static event RestartStartGameEvent OnRestartStartGameEvent;

        #region UNITY METHODS
        private void Start()
        {
            Invoke("InstantiateBall", 2f);

            ThirdPersonMovement.OnPlayerPositionEvent += DetectShootingZone;
            SwishDetector.OnTriggerEnterEvent += IncreaseScoreToScoreboard;
            TimerController.OnGameOverEvent += InstantiateBallBecauseTimerEnds;
            ThirdPersonMovement.OnStartGameEvent += StartGame;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
        #endregion

        #region PRIVATE METHODS
        private void InstantiateBall()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            ball.transform.position = BallPool.Instance.gameObject.transform.position;
        }

        private void StartGame()
        {
            _timer.SetGameIsStarted(true);
            _chronometer.SetGameIsStarted(true);
        }

        /// <summary>
        /// Calculates the distance between a player and a basket.
        /// Then sets _amountShootingZone based on whether the distance is less than or equal to _tripleScoringZone or greater than or equal to _tripleScoringZone
        /// </summary>
        /// <param name="player"></param>
        private void DetectShootingZone(GameObject player)
        {
            float distanceToBasket = DistanceBetweenTwoObjects.CalculateDistanceBetweenObjects(player.transform, SwishDetector.swishPosition);
            Debug.Log("Distance between player and basket is: " + distanceToBasket);

            switch (distanceToBasket)
            {
                case <= _tripleScoringZone:
                    _amountShootingZone = 2;
                    break;

                case >= _tripleScoringZone:
                    _amountShootingZone = 3;
                    break;
            }
        }

        private void IncreaseScoreToScoreboard()
        {
            _score.IncreaseScore(_amountShootingZone);
            InstantiateBall();
        }

        private void InstantiateBallBecauseTimerEnds()
        {
            GameObject ball = BallPool.Instance.RequestBall();
        }

        private void OnDisable()
        {
            ThirdPersonMovement.OnPlayerPositionEvent -= DetectShootingZone;
            SwishDetector.OnTriggerEnterEvent -= IncreaseScoreToScoreboard;
            TimerController.OnGameOverEvent -= InstantiateBallBecauseTimerEnds;
            ThirdPersonMovement.OnStartGameEvent -= StartGame;
        }
        #endregion

        #region PUBLIC METHODS
        public void RestartGame()
        {
            _timer.ResetTime();
            _chronometer.ResetTime();
            _score.ResetScore();
            OnRestartStartGameEvent?.Invoke();
            InstantiateBall();
        }
        #endregion


    }
}

