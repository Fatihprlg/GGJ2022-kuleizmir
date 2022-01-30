using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    bool removed = false;
    [SerializeField] GameObject Light;

    private void OnTriggerEnter(Collider other)
    {
        Light.GetComponent<Light>().enabled = true;
    }
}
