using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
    public GameObject projectile;
    public float attackInterval;
    public NavMeshFollowTarget moveScript;

    private Animator animator;
    private int animIDisAttacking;

    public Transform projectileStartPos;

    private AttackingState attackingState;

    private float timeBetweenAttack;

    private bool readyToAttack;
    private bool attacking;

    public bool hasAttacked;

    public AudioClip attackSound;

    private AudioManager aM;
    AudioSource audioSource;

    void Start() 
    {
        aM = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();

        if (attackSound == null)
        {
            attackSound = aM.sfxMeleeAttack;
        }

        moveScript = GetComponent<NavMeshFollowTarget>();
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

    private void Attack(object sender, EventArgs e)
    {
        readyToAttack = false;
        attacking = true;

        if (animator != null)
            animator.SetBool(animIDisAttacking, attacking);

        if (attackSound != null)
            audioSource.PlayOneShot(attackSound, 0.7F);

        Invoke("ResetAttack", timeBetweenAttack);
    }

    public void HitEvent()
    {
        Instantiate(projectile, projectileStartPos.position, transform.rotation); //shoot arrow
        ResetAttack();
    }

    private void OnDestroy()
    {
        attackingState.OnAttack -= Attack;
    }

    private void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;
        if (animator != null)
            animator.SetBool(animIDisAttacking, false);
    }
}
