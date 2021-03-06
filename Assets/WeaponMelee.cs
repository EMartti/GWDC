using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponMelee : MonoBehaviour
{
    private AudioManager aM;
    AudioSource audioSource;

    public AudioClip hitSound;
    public AudioClip attackSound;

    public LayerMask whatIsEnemies;
   
    public int hitDamage;
    public float hitForce;
    public int baseDamage = 50;

    public GameObject hitEffect;

    public string enemyTag = "Enemy";

    public GameObject sparkPos;

    public bool canDamage;

    private Player player;

    [SerializeField] private bool useCustomCollider;

    private void Start()
    {
        player = Player.Instance;

        audioSource = GetComponent<AudioSource>();

        aM = AudioManager.Instance;

        if (hitSound == null)
        {
            hitSound = aM.sfxMeleeHit;
        }
        if (attackSound == null)
        {
            attackSound = aM.sfxMeleeAttack;
        }

        baseDamage = hitDamage;



    }

    public void PlayAttackSound()
    {
        if (attackSound != null) AudioSource.PlayClipAtPoint(attackSound, transform.position, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(enemyTag) && canDamage)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(hitDamage + player.damageBonus, gameObject);
            }

            if (hitEffect != null) Instantiate(hitEffect, other.gameObject.transform.position, Quaternion.identity);


            if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position, 4f);
        }
    }
}