using Algoritcom.TechnicalTest.BallSpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algoritcom.TechnicalTest.Timer;
using Algoritcom.TechnicalTest.Chronometer;
using Algoritcom.TechnicalTest.Character;

namespace Algoritcom.TechnicalTest.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TimerController timer;
        [SerializeField] private ChronometerController chronometer;


        private void Start()
        {
            Invoke("StartGame", 2f);
            PlayerController.OnPlayerShoot += RespawnBall;
        }

        private void StartGame()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            ball.transform.position = BallPool.Instance.gameObject.transform.position;
            timer.SetGameIsStarted(true);
            chronometer.SetGameIsStarted(true);
        }

        private void RespawnBall()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            ball.transform.position = BallPool.Instance.gameObject.transform.position;
        }
    }
}

