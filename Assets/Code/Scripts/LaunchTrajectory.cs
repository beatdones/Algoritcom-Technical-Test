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

        [SerializeField] private Parabola _parabola;

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

            _parabola.CalculateFlyingTime();
            _parabola.KinematicMovement(true);
            _parabola.Shooting(true);

            Invoke("Disable", 5f);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
            GameObject ball = BallPool.Instance.RequestBall();
        }

        public void SetThrowingForce(float throwingForce)
        {
            _throwingForce = throwingForce;
        }

       
    }
}

