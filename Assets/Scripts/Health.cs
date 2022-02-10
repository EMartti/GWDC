using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;    
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    public event EventHandler OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(hurtSound);

        currentHealth -= damage;
        if (currentHealth <= 0) { currentHealth = 0; OnDeath?.Invoke(this, EventArgs.Empty); }


        
    }    
}
