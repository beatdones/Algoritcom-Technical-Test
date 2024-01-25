using Algoritcom.TechnicalTest.BallSpawn;
using Algoritcom.TechnicalTest.Score;
using Algoritcom.TechnicalTest.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.Ball
{
    public class BasketBall : MonoBehaviour
    {
        public UnityEvent OnColisionEvent;

        private void OnEnable()
        {
            ResetBallValues();

            TimerController.OnGameOverEvent += Disable;

            SwishDetector.OnTriggerEnterEvent += Disable;
        }

        private void OnDisable()
        {
            TimerController.OnGameOverEvent -= Disable;
            SwishDetector.OnTriggerEnterEvent -= Disable;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnColisionEvent?.Invoke();
        }

        private void ResetBallValues()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}

