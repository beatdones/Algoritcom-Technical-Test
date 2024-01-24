using Algoritcom.TechnicalTest.BallSpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.Ball
{
    public class BasketBall : MonoBehaviour
    {
        [SerializeField] private string _tagName;

        private bool isDestroyed;
        private bool wasLaunched;

        public bool WasLaunched { set { wasLaunched = value; } }

        public UnityEvent OnColisionEvent;

        private void OnEnable()
        {
            ResetBallValues();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(_tagName) && wasLaunched)
            {
                if (isDestroyed) return;

                Invoke("Disable", 5f);
                GameObject ball = BallPool.Instance.RequestBall();
                isDestroyed = true;
                wasLaunched = false;
                this.gameObject.tag = "Untagged";
            }

            OnColisionEvent?.Invoke();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void ResetBallValues()
        {
            wasLaunched = false;
            isDestroyed = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            this.gameObject.tag = "Ball";
        }
    }
}

