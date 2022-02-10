using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject deathExplosion;
    [SerializeField] private float deathDelay = 0f;
    private Health health;


    void Start()
    {
        Health.OnDeath += Health_OnDeath;
    }

    public void Health_OnDeath(Health sender)
    {
        if(sender != null)
        {
            if (sender.gameObject == gameObject)
            {
                health = sender;
                Invoke("Death", deathDelay);
            }       
        }
    }

    private void Death()
    {
        if (deathExplosion != null)
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Health.OnDeath -= Health_OnDeath;
        Destroy(gameObject);
    }
    
}
