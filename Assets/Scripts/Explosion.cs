using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SphereCollider ExplosionCol;
    public bool Destroyed = false;
    public float maxSize = 0.8f;
    List<GameObject> DestroyableObjects = new List<GameObject>();

    private void Awake()
    {
        ExplosionCol = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(0.3f, 0, 0.3f) * Time.deltaTime;
            
        }
        if (ExplosionCol.radius <= 2.5f)
        {
            ExplosionCol.radius += .1f;
        }
        if (Destroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            GameObject box = other.gameObject;
            DestroyableObjects.Add(box);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("asndkl");
        foreach(GameObject destroyablebox in DestroyableObjects)
        {
            destroyablebox.GetComponent<Rigidbody>().AddExplosionForce(300,transform.position,5, 3.0f, ForceMode.Force);
           // Destroy(destroyablebox);
        }
    }
}
