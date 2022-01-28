using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(FreezeObject());
    }

    IEnumerator FreezeObject()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(4);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(1);
        Destroy(this);

    }

}
