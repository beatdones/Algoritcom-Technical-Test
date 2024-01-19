using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float lookSensitivity = 5;

    private Vector2 moveVector;
    private Vector2 lookVector;

    private Vector3 rotation;


    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        rotation.y += lookVector.x * lookSensitivity * Time.deltaTime;
        transform.localEulerAngles = rotation;
    }
}
