using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeWeapon : MonoBehaviour
{
    private void Start()
    {
        //Health.OnDeath += Health_OnDeath;
    }

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    public List<Collider> HitColliders()
    {
        List<Collider> newList = new List<Collider>();

        foreach (Collider collider in GetColliders())
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
              newList.Add(collider);
            }
        }

        return newList;
    }

    private void Health_OnDeath(Health sender)
    {
        colliders.Remove(sender.gameObject.GetComponent<Collider>());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other.GetComponent<Collider>()); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
