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
    bool canMove = true;
    public static bool isTeleporting = false;

    Animator animator;
    Rigidbody rb;

    public GameObject FreezeArea;
    GameObject Freeze;


    void Awake()
    {
        playerInputs = new PlayerControls();
        playerInputs.Player1.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        playerInputs.Player1.Move.canceled += ctx => movementDirection = Vector2.zero;
        playerInputs.Player1.Skills.performed += ctx => spawnFreeze();
        playerInputs.Player1.Skills.canceled += ctx => destroyFreeze();
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

    private void OnTriggerStay(Collider other)
    {
        if (!isTeleporting)
        {
            if (other.CompareTag("PositiveSide"))
            {
                animator.SetBool("Smaller", true);
                animator.SetBool("Bigger", false);
            }
            else if (other.CompareTag("NegativeSide"))
            {
                animator.SetBool("Smaller", false);
                animator.SetBool("Bigger", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            Death();
        }
        if (other.CompareTag("Portal"))
        {
            StartCoroutine(PortalTeleportTime());
        }
       
        if (other.CompareTag("NegativeFinish"))
        {
            Debug.Log("negative gone");
            GameController.negativeIsFinished = true;
            gameObject.SetActive(false);
        }
    }

    void Movement()
    {
        horizontal = movementDirection.x * Time.deltaTime * movementSpeed;
        vertical = movementDirection.y * Time.deltaTime * movementSpeed;
        transform.position += new Vector3(horizontal, 0, vertical);
    }

    void spawnFreeze()
    {
        canMove = false;
        Freeze = Instantiate(FreezeArea, transform.position + new Vector3(0, .2f, 0), Quaternion.identity);
       
        //Freeze.transform.parent = transform;
    }
    void destroyFreeze()
    {
        //this.Freeze.GetComponent<Freeze>().DestroyObject();
        Destroy(Freeze);
        //GameObject.FindGameObjectWithTag("Explosion").GetComponent<Explosion>().Destroyed = true;
        canMove = true;
    }
    void Death()
    {
        GameObject.Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PortalTeleportTime()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(0.2f);
        isTeleporting = false;
    }


}
