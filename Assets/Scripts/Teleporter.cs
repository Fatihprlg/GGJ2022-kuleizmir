using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //Player1Controller player;
    public GameObject otherPortal;
    public Vector3 offset;

    private void OnTriggerEnter(Collider other)
    {
        //player = other.GetComponent<Player1Controller>();
        if (other.CompareTag("Player1") || other.CompareTag("Player"))
        {
            other.transform.position = otherPortal.transform.position + offset;
            StartCoroutine(PortalWaitTime());
            //Player1Controller.isTeleporting = false;
        }
    }

    IEnumerator PortalWaitTime()
    {
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = true; ;
    }
}
