using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject ballPrefab;
    public int poolSize = 1;

    private List<GameObject> pool;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            pool.Add(ball);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        GameObject ball = Instantiate(ballPrefab);
        ball.SetActive(false);
        pool.Add(ball);

        return ball;
    }
}