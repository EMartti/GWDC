using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject deathExplosion;

    void Start()
    {
        Health health = GetComponent<Health>();
        health.OnDeath += Health_OnDeath;
    }

    private void Health_OnDeath(object sender, EventArgs e)
    {
        if (deathExplosion != null)
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
