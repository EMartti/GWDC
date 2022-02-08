using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    public event EventHandler OnDeath;

    void Start()
    {
        currentHealth = maxHealth;        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) { currentHealth = 0; OnDeath?.Invoke(this, EventArgs.Empty); }
        
    }    
}
