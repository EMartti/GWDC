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
    public delegate void CharacterEventHandler(Health e);
    public static event CharacterEventHandler OnDeath;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            if(hurtSound != null)
                audioSource.PlayOneShot(hurtSound);
            currentHealth -= damage;
            if (currentHealth <= 0 && !isDead)
            {
                currentHealth = 0;
                isDead = true;
                OnDeath(this);
            }            
        }
    }    
}
