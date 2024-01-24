using UnityEngine;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.Score
{
    public class SwishDetector : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] public string tagToActivate;

        [Header("Actions")]
        public UnityEvent OnTriggerEnterActions;
        public UnityEvent OnTriggerExitActions;

        public delegate void TriggerEnterEvent();
        public static event TriggerEnterEvent OnTriggerEnterEvent;


        private void OnTriggerEnter(Collider collide)
        {
            if (!checkValidTrigger(collide))
                return;

            Vector3 dir = collide.transform.position - transform.position;

            if (dir.y >= 0)
            {
                OnTriggerEnterActions?.Invoke();
                OnTriggerEnterEvent?.Invoke();
            }
        }

        private bool checkValidTrigger(Collider other)
        {
            if (!string.IsNullOrEmpty(tagToActivate))
            {
                if (other.CompareTag(tagToActivate))
                {
                    return true;
                }
            }

            return false;
        }


    }
}

