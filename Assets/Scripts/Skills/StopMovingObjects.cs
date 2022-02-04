using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingObjects : MonoBehaviour
{
    MovingObject otherScript;
    public bool isFrozen = false;
    bool canbefrozen = true;
    // Start is called before the first frame update
    private void Awake()
    {
        otherScript = GetComponent<MovingObject>();
    }

    private void Update()
    {
        if (isFrozen)
        {
            if (canbefrozen)
            {
                StartCoroutine(Frozen());
            }
        }
    }

    IEnumerator Frozen()
    {
        canbefrozen = false;
        otherScript.enabled = false;
        yield return new WaitForSeconds(3f);
        otherScript.enabled = true;
        isFrozen = false;
        canbefrozen = true;
    }
}
