using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
{
    PlayerControls playerInputs;

    Vector2 movementDirection;

    float horizontal, vertical;
    public float movementSpeed = 16f;
    Rigidbody rb;

    void Awake()
    {
    
        playerInputs = new PlayerControls();
        playerInputs.Player1.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        playerInputs.Player1.Move.canceled += ctx => movementDirection = Vector2.zero;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        horizontal = movementDirection.x * Time.deltaTime * movementSpeed;
        vertical = movementDirection.y * Time.deltaTime * movementSpeed;
        transform.position += new Vector3(horizontal, 0, vertical);
        /*rb.AddForce(new Vector3( horizontal, 0, vertical));*/
    }


    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
