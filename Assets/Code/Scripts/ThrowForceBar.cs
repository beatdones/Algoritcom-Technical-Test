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
        [SerializeField] private Image powerBar;

        [SerializeField] private float power;

        [SerializeField] private const float maxPower = 15f;

        [SerializeField] private float powerCost;

        private bool emptyBar, fullBar;

        private Camera mainCamera;

        public float Power { get { return power; } }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            power = 0f;
        }

        private void Update()
        {
            CheckPowerBarStatus();
            DrawPowerBarProgress();
        }
        private void LateUpdate()
        {
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }

        private void CheckPowerBarStatus()
        {
            switch (power)
            {
                case  >= maxPower:
                    emptyBar = false;
                    fullBar = true;
                    power = maxPower;
                    break;

                case <= 0:
                    emptyBar = true;
                    fullBar = false;
                    power = 0;
                    break;
            }
        }

        private void DrawPowerBarProgress()
        {
            if (emptyBar) power += powerCost * Time.deltaTime;
            if (fullBar)  power -= powerCost * Time.deltaTime;

            powerBar.fillAmount = power / maxPower;
        }
    }
}

