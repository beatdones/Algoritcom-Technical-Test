using Algoritcom.TechnicalTest.BallSpawn;
using Algoritcom.TechnicalTest.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Ball
{
    public class LaunchTrajectory : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private float _throwingForce = .01f;

        private void OnEnable()
        {
            PlayerController.OnPlayerShoot += Resetposition;
        }

        private void Resetposition(Transform newPosition)
        {
            transform.position = newPosition.position;
            transform.forward = newPosition.forward;

            Impluse();
        }

        private void Impluse()
        {
            _rigidbody.AddForce(transform.forward * _throwingForce, ForceMode.Impulse);
            Invoke("Disable", 2f);
        }

        private void Disable()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
            GameObject ball = BallPool.Instance.RequestBall();
        }

        public void SetThrowingForce(float throwingForce)
        {
            _throwingForce = throwingForce;
        }

       
    }
}

