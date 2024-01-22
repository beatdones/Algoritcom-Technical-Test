using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.BallSpawn
{
    public class BallSpawner : MonoBehaviour
    {
        private void SpawnBall()
        {
            GameObject ball = BallPool.Instance.RequestBall();
            ball.transform.position = transform.position;
        }
    }
}

