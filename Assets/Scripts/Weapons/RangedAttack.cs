using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
    public GameObject projectilePrefab;
    public float attackInterval;
    private NavMeshFollowTarget moveScript;

    private Animator animator;
    private int animIDisAttacking;

    public Transform projectileStartPos;

    private AttackingState attackingState;

    private float timeBetweenAttack = 1f;

    [SerializeField] private float shootForce;

    private bool attacking;

    public bool hasAttacked;

    public AudioClip attackSound;

    private AudioManager aM;
    AudioSource audioSource;

    private Transform target;

    void Start() 
    {
        aM = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();

        target = Player.Instance.transform;

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
        if (!attacking)
        {
            attacking = true;

            if (animator != null)
                animator.SetBool(animIDisAttacking, attacking);

            if (attackSound != null)
                audioSource.PlayOneShot(attackSound, 0.7F);

            Invoke("ResetAttack", timeBetweenAttack);
        }
    }

    public void HitEvent()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileStartPos.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * shootForce, ForceMode.Impulse);
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
