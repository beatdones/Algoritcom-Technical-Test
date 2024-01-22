using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string tagName;

    public UnityEvent OnBallEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagName)
        {
            OnBallEnterTrigger.Invoke();
        }
    }
}
