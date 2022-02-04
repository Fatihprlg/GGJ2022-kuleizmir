using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //Player1Controller player;
    public GameObject otherPortal;
    public Vector3 offset;
    public bool isGreen;
    public bool isCommon;
    public bool isRed;
    public bool isBlue;
    Collector collector;

    private void OnTriggerEnter(Collider other)
    {
        collector = other.transform.GetChild(0).GetComponent<Collector>();
        if (isCommon)
        {
            //player = other.GetComponent<Player1Controller>();
            if (other.CompareTag("Player1") || other.CompareTag("Player"))
            {
                if (isGreen)
                {
                    //Debug.Log("collector: " + collector.tag);
                    other.transform.GetChild(0).DetachChildren();
                    if(collector != null)
                        collector.take = false;
                }
                other.transform.position = otherPortal.transform.position + offset;
                StartCoroutine(PortalWaitTime());
                //Player1Controller.isTeleporting = false;
            }
        }
        if (isBlue)
        {
            //player = other.GetComponent<Player1Controller>();
            if (other.CompareTag("Player") )
            {
                if (isGreen)
                {
                    other.transform.GetChild(0).DetachChildren();
                    if (collector != null)
                        collector.take = false;
                }
                other.transform.position = otherPortal.transform.position + offset;
                StartCoroutine(PortalWaitTime());
                //Player1Controller.isTeleporting = false;
            }
        }
        if (isRed)
        {
            //player = other.GetComponent<Player1Controller>();
            if (other.CompareTag("Player1"))
            {
                if (isGreen)
                {
                    other.transform.GetChild(0).DetachChildren();
                    if (collector != null)
                        collector.take = false;
                }
                other.transform.position = otherPortal.transform.position + offset;
                StartCoroutine(PortalWaitTime());
                //Player1Controller.isTeleporting = false;
            }
        }
    }

    IEnumerator PortalWaitTime()
    {
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        otherPortal.gameObject.GetComponent<MeshCollider>().enabled = true; ;
    }
}
