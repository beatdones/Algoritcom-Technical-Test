using Algoritcom.TechnicalTest.ThirPersonController;
using Cinemachine;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Character
{
    public class FreeCameraReferences : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

        private void Start()
        {
            ThirdPersonMovement.OnPlayerIsInstantiate += CameraReference;
        }

        private void CameraReference(Transform followTarget)
        {
            _cinemachineFreeLook.Follow = followTarget;
            _cinemachineFreeLook.LookAt = followTarget;
        }

        private void OnDisable()
        {
            ThirdPersonMovement.OnPlayerIsInstantiate -= CameraReference;

        }
    }

}
