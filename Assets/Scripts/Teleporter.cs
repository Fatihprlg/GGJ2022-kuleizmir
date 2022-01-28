using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    Player1Controller player;
    public GameObject otherPortal;

    private void OnTriggerExit(Collider other)
    {
            player = other.GetComponent<Player1Controller>();
            player.transform.position = otherPortal.transform.position;
            StartCoroutine(PortalWaitTime());
            Player1Controller.isTeleporting = false;
    }

    IEnumerator PortalWaitTime()
    {
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = true; ;
    }
}
