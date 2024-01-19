using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinCharacterController : MonoBehaviour
{

    [Space(5)]
    [Header("Input Controls")]
    float inputZ;
    float inputX;

    // [Range(0.0f, 1.0f)]
    float desiredRotationSpeed = 0.08f;
    Vector3 desiredMoveDirection;

    CapsuleCollider col;

    Animator animator;
    float speed = 1.0f;
    float allowPlayerRotation = 0.0f;

    public LayerMask groundLayer;
    public bool grounded = false;


    Rigidbody rb;


    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        InputMagnitude(); //Desplacamientos y rotaciones
    }


    private void FixedUpdate()
    {
        grounded = IsGrounded();
    }


    private bool IsGrounded()
    {
        // Para comprobar el IsGorunder por malla de colision y no por Ray cast
        return Physics.CheckCapsule(col.bounds.center,
                                    new Vector3(col.bounds.center.x, col.bounds.center.y - (col.height / 2.0f), col.bounds.center.z), //Origen y destino
                                    col.radius,   //Radio
                                    groundLayer); //Layer afectada
    }


    void InputMagnitude()
    {
        //Sacamos los inputs
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", inputX, 0.0f, Time.deltaTime);
        animator.SetFloat("InputZ", inputZ, 0.0f, Time.deltaTime);

        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        if(speed > allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", speed, 0.0f,Time.deltaTime);
            PlayerRotation();
        }
        else if(speed < allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        }
    }


    void PlayerRotation()
    {
        Camera camera = Camera.main;
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        desiredMoveDirection = forward * inputZ + right * inputX;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

}
