using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SphereCollider ExplosionCol;
    public bool Destroyed = false;
    public float maxSize = 0.4f;
    List<GameObject> DestroyableObjects = new List<GameObject>();

    private void Awake()
    {
        ExplosionCol = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
        }
        if (Destroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyable"))
        {
            GameObject box = other.GetComponent<BoxScript>().gameObject;
            DestroyableObjects.Add(box);
            Debug.Log("added to List");
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject destroyablebox in DestroyableObjects)
        {
            destroyablebox.GetComponent<BoxScript>().Destroy();
        }
    }
}
