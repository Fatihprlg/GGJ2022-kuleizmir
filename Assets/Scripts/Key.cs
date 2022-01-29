using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject plug;
    public GameObject key;
    public Keys keyType;
    bool isGoing = false;
    public float speed = 8;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if (key.transform.parent == null)
            if (keyType == Keys.NegativeKey)
            {
                Debug.Log(key.transform.parent == null);
                if (other.CompareTag("NegativePlug"))
                {
                    Debug.LogError(plug.transform.position);
                    key.transform.SetParent(plug.transform.parent);
                    key.GetComponent<MeshCollider>().enabled = false;
                    isGoing = true;
                    
                }
            }
            else if (keyType == Keys.PositiveKey)
            {
                if (other.CompareTag("PositivePlug"))
                {
                    Debug.LogError(plug.transform.position);
                    key.transform.SetParent(plug.transform.parent);
                    key.GetComponent<MeshCollider>().enabled = false;
                    isGoing = true;
                }
            }
    }


    private void Update()
    {
        if (isGoing)
        {
            key.transform.localPosition = Vector3.MoveTowards(key.transform.localPosition, Vector3.zero, speed * Time.deltaTime);
            Debug.Log(key.transform.localPosition);
        }
        if (key.transform.localPosition.Equals(Vector3.zero))
        {
            isGoing = false;
            Destroy(this.gameObject);
        }
    }


}
