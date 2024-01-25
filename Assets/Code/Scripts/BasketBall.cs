using Algoritcom.TechnicalTest.BallSpawn;
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

        public void Disable()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            gameObject.SetActive(false);
        }
    }
}

