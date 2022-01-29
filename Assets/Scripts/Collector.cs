using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
   
    public Players player;
    PlayerControls playerInputs;
    bool take = false;


    private void Awake()
    {
        playerInputs = new PlayerControls();
        if(player == Players.Player2)
            playerInputs.Player2.Take.performed += ctx => take = !take;
        else if(player == Players.Player1)
            playerInputs.Player1.Take.performed += ctx => take = !take;
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (take && (other.CompareTag("Portable") || other.CompareTag("NegativeKey")) && transform.childCount == 0)
        {
            other.transform.parent = transform;
            other.transform.localPosition = new Vector3(0, 0, 1);
        }
        else if (!take)
        {
            transform.DetachChildren();
        }
    }
}
public enum Players
{
    Player1,
    Player2
}
