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

    private Knockback kb;

    [SerializeField] private Class characterClass;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (GetComponent<CharacterClass>() != null)
            characterClass = GetComponent<CharacterClass>().characterClass;

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
            if (GetComponent<CharacterClass>() != null)
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
        Class attackerClass = attacker.GetComponent<CharacterClass>().characterClass;
        float damageMult = 1f;

        if (attackerClass == Class.Mage && characterClass == Class.Melee) { damageMult = 1.3f; return damageMult; }

        if (attackerClass == Class.Melee && characterClass == Class.Range) { damageMult = 1.3f; return damageMult; }

        if (attackerClass == Class.Range && characterClass == Class.Mage) { damageMult = 1.3f; return damageMult; }

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
