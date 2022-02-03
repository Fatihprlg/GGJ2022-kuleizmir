using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostObject : MonoBehaviour
{
    // Start is called before the first frame update
     GameObject frostEffect;

    void Awake()
    {
        //frostEffect = GameObject.FindGameObjectWithTag("Frost");
        frostEffect = Freeze.frost;
        StartCoroutine(FreezeObject());
    }

    IEnumerator FreezeObject()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject frost = Instantiate(frostEffect, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(4);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Destroy(frost);
        yield return new WaitForSeconds(1);
        Destroy(this);

    }

}
