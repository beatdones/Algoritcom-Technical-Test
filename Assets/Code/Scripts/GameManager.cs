using Algoritcom.TechnicalTest.BallSpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algoritcom.TechnicalTest.Timer;
using Algoritcom.TechnicalTest.Chronometer;
using Algoritcom.TechnicalTest.Character;
using Algoritcom.TechnicalTest.Score;

namespace Algoritcom.TechnicalTest.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TimerController _timer;
        [SerializeField] private ChronometerController _chronometer;
        [SerializeField] private ScoreController _score;

        [SerializeField] private GameObject _gameOverUI;

        private const float _tripleScoringZone = 7.3f;

        private int _amountShootingZone;

        public static bool gameIsPaused = false;


        private void Start()
        {
            Invoke("StartGame", 2f);

            PlayerController.OnPlayerPositionEvent += DetectShootingZone;

            SwishDetector.OnTriggerEnterEvent += IncreaseScoreToScoreboard;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        private void StartGame()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            ball.transform.position = BallPool.Instance.gameObject.transform.position;
            _timer.SetGameIsStarted(true);
            _chronometer.SetGameIsStarted(true);

            TimerController.OnGameOverEvent += EndGame;
        }


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
        }

        private void EndGame()
        {
            EnableOrDisableGameOverUI(true);
        }

        private void EnableOrDisableGameOverUI(bool isGameOver)
        {
            _gameOverUI.SetActive(isGameOver);
        }

        public void RestartGame()
        {
            _timer.ResetTime();
            _chronometer.ResetTime();
            _score.ResetScore();
            EnableOrDisableGameOverUI(false);
        }



    }
}

