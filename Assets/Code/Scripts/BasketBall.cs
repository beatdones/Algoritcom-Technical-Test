using Algoritcom.TechnicalTest.BallSpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    [SerializeField] private string _tagName;

    private bool isDestroyed;
    private bool wasLaunched;

    public bool WasLaunched { set { wasLaunched = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(_tagName) && wasLaunched)
        {
            if (isDestroyed) return;

            Invoke("Disable", 5f);
            GameObject ball = BallPool.Instance.RequestBall();
            isDestroyed = true;
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
