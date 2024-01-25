using TMPro;
using System;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Score
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private int _currentScore;

        public void IncreaseScore(int amount)
        {
            _currentScore = Int32.Parse(_scoreText.text);
            _currentScore += amount;
            _scoreText.text = _currentScore.ToString();
        }

        public void ResetScore()
        {
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
        }
    }
}

