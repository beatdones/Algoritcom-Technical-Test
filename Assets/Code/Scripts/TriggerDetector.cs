using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string _tagName;

    public UnityEvent OnBallEnterTrigger;

    public delegate void BallEnterEvent(GameObject ball);
    public static event BallEnterEvent OnBallEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _tagName)
        {
            OnBallEnterTrigger.Invoke();
            OnBallEnterEvent?.Invoke(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
