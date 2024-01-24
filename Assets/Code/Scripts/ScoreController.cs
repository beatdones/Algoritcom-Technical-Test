using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Score
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void IncreaseScore(int amount)
        {
            int currentScore = Int32.Parse(_scoreText.text);

            currentScore += amount;

            _scoreText.text = currentScore.ToString();
        }
    }
}

