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

    public int currentComboClip = 0;
    public bool inComboTime = false;
    public bool attacking = false;
    public Transform swordCastTransform;
    public LayerMask groundLayer;
    public bool grounded = false;

    public float jumpForce = 10.0f;

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
        Attack(); // Attaques ...
    }


    private void FixedUpdate()
    {
        grounded = IsGrounded();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Para el primer ataque
            if(attacking == false)
            {
                currentComboClip = 0;
                inComboTime = false;
                animator.SetInteger("AttackType", 0);
                animator.SetTrigger("Attack");
            }

            if(inComboTime == true)
            {
                currentComboClip += 1;
                inComboTime = false;
            }
        }
    }


    private bool IsGrounded()
    {
        // Para comprobar el IsGorunder por malla de colision y no por Ray cast
        return Physics.CheckCapsule(col.bounds.center,
                                    new Vector3(col.bounds.center.x, col.bounds.center.y - (col.height / 2.0f), col.bounds.center.z), //Origen y destino
                                    col.radius,   //Radio
                                    groundLayer); //Layer afectada
    }


    private void LateUpdate()
    {
        animator.ResetTrigger("Attack");

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sword-Attack-R1") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Sword-Attack-R2") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Sword-Attack-R3"))
          {
            attacking = true;
        }
        else
        {
            attacking = false;
            currentComboClip = 0;
        }
    }


    //public void Hit()
    //{
    //    inComboTime = true;

    //    Collider[] hitColliders = Physics.OverlapSphere(swordCastTransform.position, 2.0f);

    //    for (int i = 0; i < hitColliders.Length; i++)
    //    {
    //        if (hitColliders[i].gameObject.CompareTag("Enemy"))
    //        {
    //            hitColliders[i].gameObject.GetComponent<EnemyController>().SetDamage(10);

    //            //Impulsar Enemy hacia atras
    //            //hitColliders[i].gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 200f,ForceMode.Impulse);
    //        }
    //    }
    //}


    public void ComboCheck(int nextAnim)
    {
        inComboTime = false;
        if(nextAnim == 1 && currentComboClip > nextAnim - 1)
        {
            animator.Play("Sword-Attack-R2");
        }
        else if(nextAnim == 2 && currentComboClip > nextAnim - 1)
        {
            animator.Play("Sword-Attack-R3");
        }
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

        //Salto por rigidbody
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
