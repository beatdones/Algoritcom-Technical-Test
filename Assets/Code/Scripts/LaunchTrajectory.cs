using UnityEngine;
using Algoritcom.TechnicalTest.Character;
using Algoritcom.TechnicalTest.ThirPersonController;

namespace Algoritcom.TechnicalTest.Ball
{
    public class LaunchTrajectory : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BasketBall _basketBall;

        private float _throwingForce = 10f;


        private void OnEnable()
        {
            ThirdPersonMovement.OnPlayerShoot += Resetposition;
        }

        private void Resetposition(Transform newPosition, float powerUp)
        {
            transform.position = newPosition.position;
            transform.forward = newPosition.forward;
            _throwingForce = powerUp;

            Impulse();
        }

        private void Impulse()
        {
            _rigidbody.velocity = transform.forward * _throwingForce;
        }

        private void OnDisable()
        {
            ThirdPersonMovement.OnPlayerShoot -= Resetposition;
        }

        public void SetThrowingForce(float throwingForce)
        {
            _throwingForce = throwingForce;
        }

       
    }
}

