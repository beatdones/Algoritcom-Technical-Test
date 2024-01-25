using Algoritcom.TechnicalTest.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Algoritcom.TechnicalTest.Ball
{
    public class ThrowForceBar : MonoBehaviour
    {
        [SerializeField] private Image _powerBar;
        [SerializeField] private float _power;
        [SerializeField] private float _powerCost;

        private const float _maxPower = 15f;
        private bool _emptyBar, _fullBar;
        private Camera _mainCamera;

        public float Power { get { return _power; } }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _power = 0f;
        }

        private void Update()
        {
            CheckPowerBarStatus();
            DrawPowerBarProgress();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCamera.transform.forward);
        }

        /// <summary>
        /// Checks the status of a power bar based on a power variable.
        /// If the power is equal to or exceeds the maximum allowed, it sets the bar to full.
        /// If the power is zero or negative, it sets the bar to empty.
        /// The power is adjusted accordingly in both cases.
        /// </summary>
        private void CheckPowerBarStatus()
        {
            switch (_power)
            {
                case  >= _maxPower:
                    _emptyBar = false;
                    _fullBar = true;
                    _power = _maxPower;
                    break;

                case <= 0:
                    _emptyBar = true;
                    _fullBar = false;
                    _power = 0;
                    break;
            }
        }

        /// <summary>
        /// Responsible for updating the power bar's visual representation.
        /// If the bar is empty, it increases the power based on a cost and time.
        /// If the bar is full, it decreases the power similarly.
        /// The _powerBar.fillAmount is then updated based on the current power relative to the maximum allowed.
        /// </summary>
        private void DrawPowerBarProgress()
        {
            if (_emptyBar) _power += _powerCost * Time.deltaTime;
            if (_fullBar)  _power -= _powerCost * Time.deltaTime;

            _powerBar.fillAmount = _power / _maxPower;
        }
    }
}

