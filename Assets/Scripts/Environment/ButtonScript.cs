using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool isHeavy;
    public bool isNormal;
    public bool isLight;

    public bool isPushedDown = false;
    bool removed = false;
    [SerializeField] GameObject Object;
    [SerializeField] Vector3 movePosition;
    [SerializeField] float speed = 4;
    Vector3 firstPos;

    private void OnTriggerEnter(Collider other)
    {
        firstPos = Object.transform.position;
        removed = false;
        if (isHeavy)
        {
            if (other.CompareTag("Interactable"))
            {
                isPushedDown = true;
            }
        }
        else if (isNormal)
        {
            if(other.CompareTag("Player")|| other.CompareTag("Interactable") || other.CompareTag("Player1"))
            {
                isPushedDown = true;
            }
        }
        else if (isLight)
        {
            if (other.CompareTag("Player") ||  other.CompareTag("Interactable") || other.CompareTag("Player1"))
            {
                isPushedDown = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        removed = true;
        if (isHeavy)
        {
            if (other.CompareTag("Interactable"))
            {
                isPushedDown = false;
            }
        }
        else if (isNormal)
        {
            if (other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("Player1"))
            {
                isPushedDown = false;
            }
        }
    }

    private void Update()
    {
        if (isPushedDown)
            Object.transform.position = Vector3.MoveTowards(Object.transform.position, movePosition, speed * Time.deltaTime);
        else if(removed)
            Object.transform.position = Vector3.MoveTowards(Object.transform.position, firstPos, speed * Time.deltaTime);
    }
}
