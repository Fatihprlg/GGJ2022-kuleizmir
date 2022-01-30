using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    SphereCollider FreezeCol;
    public bool Destroyed = false;
    public float maxSize = 0.4f;
    List<GameObject> Freezable = new List<GameObject>();


    private void Awake()
    {
        FreezeCol = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(0.1f, 0, 0.1f) * Time.deltaTime;

        }
        if (FreezeCol.radius <= 5f)
        {
            FreezeCol.radius += .01f;
        }
        if (Destroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") || other.CompareTag("Freezable") || other.CompareTag("Destroyable"))
        {
            GameObject box = other.gameObject;
            Freezable.Add(box);
        }
    }

    private void OnDestroy()
    {
        foreach (var obj in Freezable)
        {
            StopMovingObjects frezescript = obj.GetComponent<StopMovingObjects>();
            if(frezescript != null)
            {
                frezescript.isFrozen = true;
            }
            if(obj.GetComponent<FrostObject>() == null)
                obj.AddComponent<FrostObject>();
        }
    }

/*    public void DestroyObject()
    {
        StartCoroutine(FreezeObject(Freezable));
    }*/

    
}
