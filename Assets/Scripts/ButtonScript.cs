using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool isHeavy;
    public bool isNormal;
    public bool isLight;

    public bool isPushedDown = false;


    private void OnTriggerEnter(Collider other)
    {
        if (isHeavy)
        {
            if (other.CompareTag("Interactable"))
            {
                isPushedDown = true;
            }
        }
        else if (isNormal)
        {
            if(other.CompareTag("Player")|| other.CompareTag("Interactable"))
            {
                isPushedDown = true;
            }
        }
        else if (isLight)
        {
            if (other.CompareTag("Player") ||  other.CompareTag("Interactable"))
            {
                isPushedDown = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isHeavy)
        {
            if (other.CompareTag("Interactable"))
            {
                isPushedDown = false;
            }
        }
        else if (isNormal)
        {
            if (other.CompareTag("Player") || other.CompareTag("Interactabe"))
            {
                isPushedDown = false;
            }
        }

    }

}
