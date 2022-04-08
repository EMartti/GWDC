using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    [SerializeField] private GameObject weaponPrefab;
    private GameObject currentWeapon;
    public float attackInterval;

    private Animator animator;
    private int animIDisAttacking;

    private AttackingState attackingState;

    private float timeBetweenAttack;

    private bool attacking;

    public bool hasAttacked;

    public AudioClip attackSound;

    private AudioManager aM;
    AudioSource audioSource;

    private WeaponMelee weapon;

    [SerializeField] private Transform weaponHand;
    void Start() 
    {
        aM = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();

        if (attackSound == null)
        {
            attackSound = aM.sfxMeleeAttack;
        }

        SpawnWeapon();

        attackingState = GetComponent<CharacterStateManager>().attackState;
        animator = GetComponent<Animator>();
        animIDisAttacking = Animator.StringToHash("isAttacking");

        attackingState.OnAttack += Attack;

        if (animator != null)
        {
            foreach (var item in animator.runtimeAnimatorController.animationClips)
            {
                if (item.name == "Attack")
                {
                    timeBetweenAttack = item.length;
                    break;
                }
            }
        }
    }

    private void SpawnWeapon()
    {
        currentWeapon = Instantiate(weaponPrefab, weaponHand.position, Quaternion.identity);
        currentWeapon.transform.SetParent(weaponHand);

        weapon = currentWeapon.GetComponent<WeaponMelee>();
    }

    private void Attack(object sender, EventArgs e)
    {
        attacking = true;

        if (animator != null)
            animator.SetBool(animIDisAttacking, attacking);

        if (attackSound != null)
            audioSource.PlayOneShot(attackSound, 0.7F);

        Invoke("ResetAttack", timeBetweenAttack);
    }

    public void AttackStart()
    {
        weapon.canDamage = true;
    }

    public void AttackEnd()
    {
        weapon.canDamage = false;
    }

    public void HitEvent()
    {

    }

    private void ResetAttack()
    {
        attacking = false;
        animator.SetBool(animIDisAttacking, attacking);
    }

    private void OnDestroy()
    {
        attackingState.OnAttack -= Attack;
    }
}
