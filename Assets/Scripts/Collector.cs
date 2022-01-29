using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
   
    public Players player;
    PlayerControls playerInputs;
    public static bool take = false;


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
        if (take && (other.CompareTag("Portable") || other.CompareTag(player == Players.Player1 ? "NegativeKey" : "PositiveKey")) && transform.childCount == 0)
        {
            other.transform.parent = transform;
            if(other.CompareTag(player == Players.Player1 ? "NegativeKey" : "PositiveKey"))
                other.transform.localPosition = new Vector3(0, .5f, 2);
            else
                other.transform.localPosition = new Vector3(0, 0, 1);
        }
        else if (!take)
        {
            //other.transform.parent.SetParent(null);
            transform.DetachChildren();
        }
    }
}

