using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    #region VariableClasses
    [Serializable]
    public class AudioInspector
    {
        public AudioClip hurtSound;
        public AudioClip healSound;
    }

    [Serializable]
    public class EffectsInspector
    {
        public GameObject healEffect;
        public GameObject hurtEffect;
    }
    #endregion

    [Header("Parameters")]
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private AudioInspector audio;
    [SerializeField] private EffectsInspector effect;


    private AudioManager aM;
    private AudioSource audioSource;
    public delegate void CharacterEventHandler(Health e);
    public static event CharacterEventHandler OnDeath;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        audioSource = GetComponent<AudioSource>();

        aM = AudioManager.Instance;

        if (audio.hurtSound == null) {
            audio.hurtSound = aM.sfxHurt;
        }
        if (audio.healSound == null) {
            audio.healSound = aM.sfxHeal;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead && damage > 0)
        {
            if(audio.hurtSound != null)
                audioSource.PlayOneShot(audio.hurtSound);
            if(effect.hurtEffect != null)
                Instantiate(effect.hurtEffect, new Vector3(transform.position.x, 1, transform.position.z), effect.hurtEffect.transform.rotation, gameObject.transform);
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
        if (audio.healSound != null)
            audioSource.PlayOneShot(audio.healSound);
        if (effect.healEffect != null)
            Instantiate(effect.healEffect, new Vector3 (transform.position.x, 1, transform.position.z), effect.healEffect.transform.rotation, gameObject.transform); //spawn healing particles as child
        currentHealth += healValue;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
