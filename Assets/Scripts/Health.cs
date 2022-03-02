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
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private AudioInspector audio;
    [SerializeField] private EffectsInspector effect;


    private AudioManager aM;
    private AudioSource audioSource;
    public delegate void CharacterEventHandler(Health e);
    public static event CharacterEventHandler OnDeath;

    private bool hasClass = false;

    private Knockback kb;

    [SerializeField] private CharacterClass characterClass;
    [SerializeField] private Class thisClass;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        characterClass = GetComponent<CharacterClass>();

        if (characterClass != null)
        {
            hasClass = true;
        }

        audioSource = GetComponent<AudioSource>();
        kb = GetComponent<Knockback>();


        aM = AudioManager.Instance;

        if (audio.hurtSound == null) {
            audio.hurtSound = aM.sfxHurt;
        }
        if (audio.healSound == null) {
            audio.healSound = aM.sfxHeal;
        }
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        if (!isDead && damage > 0)
        {
            if (hasClass)
                damage = Mathf.RoundToInt(damage * ClassDmg(attacker));

            if (audio.hurtSound != null)
                audioSource.PlayOneShot(audio.hurtSound);
            if(effect.hurtEffect != null)
                Instantiate(effect.hurtEffect, new Vector3(transform.position.x, 1, transform.position.z), effect.hurtEffect.transform.rotation, gameObject.transform);
            if (kb != null)
                kb.AddForce(damage, attacker.transform.position); ;
            currentHealth -= damage;
            if (currentHealth <= 0 && !isDead)
            {
                currentHealth = 0;
                isDead = true;
                if(OnDeath != null) OnDeath(this);
            }            
        }
    }

    private float ClassDmg(GameObject attacker)
    {
        float damageMult = 1f;

        CharacterClass attackerClass = attacker.GetComponent<CharacterClass>();

        if (attackerClass == null) return damageMult;        

        if (attackerClass.characterClass == Class.Mage && thisClass == Class.Melee) { damageMult = 1.3f; return damageMult; }

        if (attackerClass.characterClass == Class.Melee && thisClass == Class.Range) { damageMult = 1.3f; return damageMult; }

        if (attackerClass.characterClass == Class.Range && thisClass == Class.Mage) { damageMult = 1.3f; return damageMult; }

        return damageMult;
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
