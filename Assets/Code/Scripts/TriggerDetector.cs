using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string _tagName;

    public UnityEvent OnBallEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _tagName)
        {
            OnBallEnterTrigger.Invoke();
        }
    }
}
