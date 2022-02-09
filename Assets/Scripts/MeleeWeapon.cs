using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{    
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    public List<Collider> HitColliders()
    {
        List<Collider> newList = new List<Collider>();

        Debug.Log("hitevent");

        foreach (Collider collider in GetColliders())
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
                newList.Add(collider);
            }
        }

        return newList;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other.GetComponent<Collider>()); Debug.Log("added: " + other.gameObject.name); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
