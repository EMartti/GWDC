using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashImpact : MonoBehaviour
{

    [SerializeField] private int impactDamage = 30;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private AudioClip impactSound;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(impactDamage, gameObject);
            }

            //if (impactEffect != null) Instantiate(impactEffect, other.gameObject.transform.position, Quaternion.identity);


            if (impactSound != null) AudioSource.PlayClipAtPoint(impactSound, transform.position, 4f);
        }
    }


}
