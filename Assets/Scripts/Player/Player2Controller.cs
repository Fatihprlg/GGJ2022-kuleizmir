using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
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

    public GameObject ExplosionArea;
    GameObject Freeze;
    // Start is called before the first frame update
    void Awake()
    {
        playerInputs = new PlayerControls();
        playerInputs.Player2.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        playerInputs.Player2.Move.canceled += ctx => movementDirection = Vector2.zero;
        playerInputs.Player2.Skill.performed += ctx => spawnExplosion();
        playerInputs.Player2.Skill.canceled += ctx => destroyExplosion();
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
    // Update is called once per frame
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
        if (other.CompareTag("KillBox"))
        {
            Death();
        }
        if (other.CompareTag("Portal"))
        {
            isTeleporting = true;
        }
        if (!isTeleporting)
        {
            if (other.CompareTag("NegativeSide"))
            {

                animator.SetBool("Smaller", true);
                animator.SetBool("Bigger", false);

            }
            else if (other.CompareTag("PositiveSide"))
            {

                animator.SetBool("Smaller", false);
                animator.SetBool("Bigger", true);
            }
        }
        if (other.CompareTag("PositiveFinish"))
        {
            Debug.Log("positivegone gone");
            GameController.positiveIsFinished = true;
        }
    }

    void Movement()
    {
        horizontal = movementDirection.x * Time.deltaTime * movementSpeed;
        vertical = movementDirection.y * Time.deltaTime * movementSpeed;
        transform.position += new Vector3(horizontal, 0, vertical);

    }

    void spawnExplosion()
    {
        canMove = false;
        GameObject Explosion = Instantiate(ExplosionArea, transform.position + new Vector3(0, .2f, 0), Quaternion.identity);
        Explosion.transform.parent = transform;
    }
    void destroyExplosion()
    {
        Destroy(transform.GetChild(1).gameObject);
        //GameObject.FindGameObjectWithTag("Explosion").GetComponent<Explosion>().Destroyed = true;
        canMove = true;
    }
    void Death()
    {
        GameObject.Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
