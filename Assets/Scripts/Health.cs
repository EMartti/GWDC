using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    private AudioManager aM;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private GameObject healEffect;
    [SerializeField] private GameObject hurtEffect;
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    public delegate void CharacterEventHandler(Health e);
    public static event CharacterEventHandler OnDeath;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        audioSource = GetComponent<AudioSource>();

        aM = AudioManager.Instance;

        if (hurtSound == null) {
            hurtSound = aM.sfxHurt;
        }
        if (healSound == null) {
            healSound = aM.sfxHeal;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead && damage > 0)
        {
            if(hurtSound != null)
                audioSource.PlayOneShot(hurtSound);
            if(hurtEffect != null)
                Instantiate(hurtEffect, new Vector3(transform.position.x, 1, transform.position.z), hurtEffect.transform.rotation, gameObject.transform);
            currentHealth -= damage;
            if (currentHealth <= 0 && !isDead)
            {
                currentHealth = 0;
                isDead = true;
                if(OnDeath != null) OnDeath(this);
            }            
        }
    }   
    
    public void AddHealth(int healValue) {
        if (healSound != null)
            audioSource.PlayOneShot(healSound);
        if (healEffect != null)
            Instantiate(healEffect, new Vector3 (transform.position.x, 1, transform.position.z), healEffect.transform.rotation, gameObject.transform); //spawn healing particles as child
        currentHealth += healValue;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
