using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    PlayerControls playerInputs;
    Vector2 movementDirection;
    public Vector3 minScale;
    public Vector3 maxScale;
    float horizontal, vertical;
    public float movementSpeed = 16f;
    bool canJump = true;
    bool canMove = true;
    public static bool isTeleporting = false;


    Animator animator;
    Rigidbody rb;

    public GameObject ExplosionArea;


    void Awake()
    {
        playerInputs = new PlayerControls();
        playerInputs.Player1.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        playerInputs.Player1.Move.canceled += ctx => movementDirection = Vector2.zero;
        playerInputs.Player1.Jump.performed += ctx => Jump();
        playerInputs.Player1.Explosion.performed += ctx => spawnExplosion();
        playerInputs.Player1.Explosion.canceled += ctx => destroyExplosion();
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
        if (canMove)
        Movement();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            isTeleporting = true;
        }

        if (other.CompareTag("PositiveSide"))
        {
            if (!isTeleporting)
            {
                animator.SetBool("Smaller", true);
                animator.SetBool("Bigger", false);
            }
          
        }
        else if (other.CompareTag("NegativeSide"))
        {
            if (!isTeleporting)
            {
                animator.SetBool("Smaller", false);
                animator.SetBool("Bigger", true);
            }
           
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

    void spawnExplosion()
    {
        canMove = false;
        GameObject Explosion = Instantiate(ExplosionArea, transform.position, Quaternion.identity);
        Explosion.transform.parent = transform;
    }
    void destroyExplosion()
    {
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<Explosion>().Destroyed = true;
        canMove = true;
    }
    void Death()
    {
        GameObject.Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
