using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraReferences : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    private void Start()
    {
        CharacterController.OnPlayerIsInstantiate += CameraReference;
    }

    private void CameraReference(Transform followTarget)
    {
        cinemachineFreeLook.Follow = followTarget;
        cinemachineFreeLook.LookAt = followTarget;
    }
}
