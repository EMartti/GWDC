using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Melee : MonoBehaviour {
    
    private AudioManager aM;
    private PlayerInputActions playerInputActions;
    public Animator animator;

    public LayerMask whatIsEnemies;
    AudioSource audioSource;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    #region VariablesClasses

    [Serializable]
    public class AudioInspector
    {
        [Header("Audio")]
        public AudioClip hitSound;
        public AudioClip attackSound;
    }

    [SerializeField] private AudioInspector audio;

    [Header("Visuals")]
    [SerializeField] private GameObject weapon;
    public GameObject hitEffect;

    [Header("Parameters")]
    [SerializeField] private bool automatic;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private Transform player;

    #endregion

    private int animIDisAttacking;


    public MeleeWeapon meleeWeaponCollider;

    public int hitDamage;
    public float attackRange;
    public float hitForce;

    bool attacking, readyToAttack;
    private bool allowInvoke = true;
    private bool isPlayer = false;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();

        readyToAttack = true;

        if (gameObject.tag == "Player") isPlayer = true;
    }

    private void Start() {
        aM = AudioManager.Instance;

        if (audio.hitSound == null) {
            audio.hitSound = aM.sfxMeleeHit;
        }
        if (audio.attackSound == null) {
            audio.attackSound = aM.sfxMeleeAttack;
        }

        if (isPlayer)
        {
            playerInputActions = PlayerInputs.Instance.playerInputActions;

            playerInputActions.Player.Fire.Enable();
            playerInputActions.Player.Fire.started += OnMelee;

            animIDisAttacking = Animator.StringToHash("isAttacking");
        }        

        if(animator != null)
        {
            foreach (var item in animator.runtimeAnimatorController.animationClips)
            {
                if (item.name == "1H-RH@Attack01")
                {
                    timeBetweenAttack = item.length;
                    break;
                }
            }
        }  
    }

    #region InputSystem
    private void OnMelee(InputAction.CallbackContext obj) 
    {
        if (isPlayer)
        {
            attacking = true;

            if (readyToAttack && attacking)
            {
                Attack();
            }
            attacking = false;
        }        
    }

    private void OnDisable() 
    {
        if(isPlayer)
            playerInputActions.Player.Fire.Disable();
    }
    #endregion

    public void Attack() {
        readyToAttack = false;

        if(animator != null)
        animator.SetBool(animIDisAttacking, attacking);


        if (allowInvoke) {
            Invoke("ResetAttack", timeBetweenAttack);
            allowInvoke = false;
        }

        if (audio.attackSound != null)
            audioSource.PlayOneShot(audio.attackSound, 0.7F);
        //AudioFW.PlayRandomPitch("sfx_player_melee_atk");
    }
    public void HitEvent()
    {
        foreach (Collider collider in meleeWeaponCollider.HitColliders())
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.TakeDamage(hitDamage);
                Hit(collider);
            }
        }
    }

    private void Hit(Collider enemy) {
        if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);

        if (enemy.GetComponent<Rigidbody>())
            enemy.GetComponent<Rigidbody>().AddForce(hitForce * player.position - transform.position);

        if (audio.hitSound != null) AudioSource.PlayClipAtPoint(audio.hitSound, transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other) {
        colliders.Remove(other);
    }


    private void ResetAttack() {
        readyToAttack = true;
        allowInvoke = true;
        attacking = false;
        animator.SetBool(animIDisAttacking, false);
    }
}
