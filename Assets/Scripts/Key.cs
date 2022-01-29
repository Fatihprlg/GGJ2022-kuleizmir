using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject plug;
    public GameObject key;
    public Keys keyType;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (keyType == Keys.NegativeKey)
        {
            if (other.CompareTag("NegativePlug") && key.transform.parent == null)
            {
                Debug.LogError(plug.transform.position);
                key.transform.SetParent(plug.transform.parent);
                key.transform.localPosition = Vector3.zero;
                key.GetComponent<MeshCollider>().enabled = false;
                Destroy(this.gameObject);
            }
        }
        else if (keyType == Keys.PositiveKey)
        {
            if (other.CompareTag("PositivePlug") && key.transform.parent == null)
            {
                key.transform.SetParent(plug.transform.parent);
                key.transform.localPosition = Vector3.zero;
                key.GetComponent<MeshCollider>().enabled = false;
                Destroy(this.gameObject);
            }
        }
    }


}
