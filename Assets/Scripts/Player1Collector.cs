using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Collector : MonoBehaviour
{
    PlayerControls playerInputs;
    bool take = false;


    private void Awake()
    {
        playerInputs = new PlayerControls();
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
        if (take && other.CompareTag("Portable") && transform.childCount == 0)
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
