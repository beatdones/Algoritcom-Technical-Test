using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region VARIABLES
    private float inputZ;
    private float inputX;

    private float desiredRotationSpeed = 0.08f;
    private Vector3 desiredMoveDirection;

    private CapsuleCollider collider;

    private Animator animator;
    private float speed = 1.0f;
    private float allowPlayerRotation = 0.0f;
    private bool isShooting;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool grounded = false;
    private object followtransform;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        InputMagnitude(); //Desplacamientos y rotaciones
        if (Input.GetButton("Fire2") && !isShooting) ChargeShoot();
        if (Input.GetButton("Fire1")) Shoot();
    }

    private void FixedUpdate()
    {
        grounded = IsGrounded();
    }
    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Checks if an object is grounded using mesh collision rather than a raycast.
    /// It employs Physics.CheckCapsule with the collider's parameters and a specified ground layer, returning a boolean result.
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(collider.bounds.center,
                                    new Vector3(collider.bounds.center.x, collider.bounds.center.y - (collider.height / 2.0f), collider.bounds.center.z),
                                    collider.radius,
                                    groundLayer);
    }

    /// <summary>
    /// Sets input values based on the horizontal and vertical axes.
    /// It updates the animator parameters, calculates speed, and adjusts the character's rotation if the speed surpasses a threshold.
    /// The method ensures smooth transitions in the animation while handling player rotation conditions.
    /// </summary>
    private void InputMagnitude()
    {
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

    /// <summary>
    /// Aligns the player's orientation with the input direction.
    /// It utilizes the main camera's forward and right vectors, creating a desired movement direction.
    /// The character smoothly rotates using Quaternion.Slerp towards the calculated direction at a specified rotation speed.
    /// </summary>
    private void PlayerRotation()
    {
        //followtransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationpower, vector3.up);

        //followtransform.transform.rotation *= Quaternion.angleaxis(_look.y * rotationpower, vector3.right);

        //var angles = followtransform.transform.localeulerangles;
        //angles.z = 0;

        //var angle = followtransform.transform.localeulerangles.x;

        ////clamp the up/down rotation
        //if (angle > 180 && angle < 340)
        //{
        //    angles.x = 340;
        //}
        //else if (angle < 180 && angle > 40)
        //{
        //    angles.x = 40;
        //}

        //followtransform.transform.localeulerangles = angles;

        //nextrotation = quaternion.lerp(followtransform.transform.rotation, nextrotation, time.deltatime * rotationlerp);




        Camera camera = Camera.main;
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        desiredMoveDirection = forward * inputZ + right * inputX;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    private void ChargeShoot()
    {
        animator.SetBool("Charge", true);
    }

    private void Shoot()
    {
        animator.SetBool("Shoot", true);
    }
    #endregion
}
