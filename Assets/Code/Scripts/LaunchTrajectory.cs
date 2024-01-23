using Algoritcom.TechnicalTest.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Ball
{
    public class LaunchTrajectory : MonoBehaviour
    {
        [SerializeField] private float _maximumStrength = 10f;
        [SerializeField] private float _maximumDistance = 20f;
        [SerializeField] private float _throwDuration = 2f;

        [SerializeField, Tooltip("Maximum error perventage")] private float _maximumErrorFactor = 0.2f;

        private Transform _tarjet;

        private void Start()
        {
            PlayerController.OnPlayerShoot += Throw;
        }

        private void Throw()
        {
            GetComponent<Rigidbody>().velocity = transform.up * _maximumStrength;
        }

        public void SetTarget(Transform target)
        {
            _tarjet = target;
        }
    }
}

