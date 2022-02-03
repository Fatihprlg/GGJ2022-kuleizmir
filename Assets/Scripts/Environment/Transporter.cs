using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public GameObject transporterObject;


    private void Update()
    {
        gameObject.transform.position = transporterObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||other.CompareTag("Player1"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player1"))
        {
            transform.DetachChildren();
        }
    }
}
