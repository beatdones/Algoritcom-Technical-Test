using UnityEngine;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.Score
{
    public class SwishDetector : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private string _tagToActivate;

        public static Transform swishPosition;

        [Header("Actions")]
        public UnityEvent OnTriggerEnterActions;
        public UnityEvent OnTriggerExitActions;

        public delegate void TriggerEnterEvent();
        public static event TriggerEnterEvent OnTriggerEnterEvent;

        private void Awake()
        {
            swishPosition = this.transform;
        }

        /// <summary>
        /// Checks if the trigger is valid, calculates the direction between the trigger and the current object, and if the direction's y-component is non-negative.
        /// </summary>
        /// <param name="collide"></param>
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

        /// <summary>
        /// Returns true if the provided collider has a tag that matches the specified _tagToActivate.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool checkValidTrigger(Collider other)
        {
            if (!string.IsNullOrEmpty(_tagToActivate))
            {
                if (other.CompareTag(_tagToActivate))
                {
                    return true;
                }
            }

            return false;
        }

    }
}

