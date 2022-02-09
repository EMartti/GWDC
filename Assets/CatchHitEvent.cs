using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchHitEvent : MonoBehaviour
{
    public Melee melee;


    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    public void HitEvent()
    {
        //melee.HitEvent();

        Debug.Log("hitevent");

        foreach (Collider collider in GetColliders())
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
                IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(melee.hitDamage);
                    //melee.Hit(collider);
                }
            }
        }
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
