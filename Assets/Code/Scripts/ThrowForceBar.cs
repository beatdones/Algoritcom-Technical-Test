using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Algoritcom.TechnicalTest.Ball
{
    public class ThrowForceBar : MonoBehaviour
    {
        [SerializeField] private Image powerBar;

        [SerializeField] private float power, maxPower;

        [SerializeField] private float powerCost;

        private bool emptyBar, fullBar;



        private void Update()
        {
            GetCurrentFill();
        }

        private void GetCurrentFill()
        {
            if (power >= maxPower)
            {
                emptyBar = false;
                fullBar = true;
                power = maxPower;
            }
            else if(power <= 0)
            {
                emptyBar = true;
                fullBar = false;
                power = 0;
            }

            if (emptyBar)
            {
                power += powerCost * Time.deltaTime;
            }
            else if (fullBar)
            {
                power -= powerCost * Time.deltaTime;
            }

            powerBar.fillAmount = power / maxPower;
        }
    }
}

