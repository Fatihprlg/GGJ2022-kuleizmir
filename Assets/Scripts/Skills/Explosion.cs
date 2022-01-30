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
        if (ExplosionCol.radius <= 4f)
        {
            ExplosionCol.radius += .01f;
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
            GameObject box = other.gameObject;
            DestroyableObjects.Add(box);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject destroyablebox in DestroyableObjects)
        {
            if (ExplosionCol.radius < 2f && destroyablebox.GetComponent<Rigidbody>().mass <= 1)
            {
                destroyablebox.transform.DetachChildren();
                Destroy(destroyablebox);
            }
            else if (ExplosionCol.radius >= 2f)
            {
                if (destroyablebox.GetComponent<Rigidbody>().mass > 1 && destroyablebox.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
                {
                    destroyablebox.transform.parent.GetComponent<BoxCollider>().isTrigger = true;
                    destroyablebox.GetComponent<Rigidbody>().isKinematic = false;
                    destroyablebox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    destroyablebox.GetComponent<Rigidbody>().AddExplosionForce(1500, transform.position, 5, 3.0f, ForceMode.Force);
                }
                else return;
                Destroy(destroyablebox, 3f);
            }
           // Destroy(destroyablebox);
        }
    }
}
