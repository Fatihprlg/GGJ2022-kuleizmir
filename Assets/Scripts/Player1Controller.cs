using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
{
    PlayerControls playerInputs;
    Vector2 movementDirection;
    public Vector3 minScale;
    public Vector3 maxScale;
    float horizontal, vertical;
    public float movementSpeed = 16f;
    bool canJump = true;

    Animator animator;
    Rigidbody rb;


    void Awake()
    {
        playerInputs = new PlayerControls();
        playerInputs.Player1.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        playerInputs.Player1.Move.canceled += ctx => movementDirection = Vector2.zero;
        playerInputs.Player1.Jump.performed += ctx => Jump();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    void Update()
    {
        if (transform.localScale.x <= minScale.x)
        {
            Death();
        }
        Movement();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PositiveGround"))
        {
            animator.SetBool("Smaller", true);
            animator.SetBool("Bigger", false);

        }
        else if (other.CompareTag("NegativeGround"))
        {
            animator.SetBool("Smaller", false);
            animator.SetBool("Bigger", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void Movement()
    {
        horizontal = movementDirection.x * Time.deltaTime * movementSpeed;
        vertical = movementDirection.y * Time.deltaTime * movementSpeed;
        transform.position += new Vector3(horizontal, 0, vertical);

    }

    void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            canJump = false;
        }
    }
    void Death()
    {
        Debug.Log("death");
    }



}
