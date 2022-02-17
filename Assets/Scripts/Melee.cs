using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Melee : MonoBehaviour {
    //public Rigidbody rb;
    public LayerMask whatIsEnemies;
    private PlayerInputActions playerInputActions;

    private AudioManager aM;
    AudioSource audioSource;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    [Header("Audio")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip attackSound;
    

    [Header("Visuals")]
    [SerializeField] private GameObject weapon;
    public Animator animator;
    public GameObject hitEffect;

    [Header("Parameters")]
    [SerializeField] private bool automatic;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private Transform player;

    private int animIDisAttacking;


    public MeleeWeapon meleeWeaponCollider;

    public int hitDamage;
    public float attackRange;
    public float hitForce;

    public float maxLifeTime;

    PhysicMaterial physics_mat;

    bool attacking, readyToAttack;

    private bool allowInvoke = true;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        readyToAttack = true;


    }

    public void PrintEvent(string s) {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }

    private void Start() {
        aM = AudioManager.Instance;

        if (hitSound == null) {
            hitSound = aM.sfxMeleeHit;
        }
        if (attackSound == null) {
            attackSound = aM.sfxMeleeAttack;
        }

        playerInputActions = PlayerInputs.Instance.playerInputActions;

        playerInputActions.Player.Fire.Enable();
        playerInputActions.Player.Fire.started += OnMelee;

        animIDisAttacking = Animator.StringToHash("isAttacking");


        foreach (var item in animator.runtimeAnimatorController.animationClips) {
            if (item.name == "1H-RH@Attack01") {
                timeBetweenAttack = item.length;
                break;
            }
        }


    }

    #region InputSystem
    private void OnMelee(InputAction.CallbackContext obj) {
        attacking = true;

        if (readyToAttack && attacking) {
            Attack();
        }
        attacking = false;
    }

    private void OnDisable() {
        playerInputActions.Player.Fire.Disable();
    }
    #endregion

    void Update() {
        //attacking = false;
        if (automatic && playerInputActions.Player.Fire.ReadValue<float>() > 0)
            attacking = true;



        //Automatic Attacking
        if (readyToAttack && attacking && automatic) {
            Attack();
        }
    }

    public void Attack() {
        readyToAttack = false;

        animator.SetBool(animIDisAttacking, attacking);


        if (allowInvoke) {
            Invoke("ResetAttack", timeBetweenAttack);
            allowInvoke = false;
        }

        if (attackSound != null)
            audioSource.PlayOneShot(attackSound, 0.7F);
        //AudioFW.PlayRandomPitch("sfx_player_melee_atk");
    }
    public void HitEvent()
    {
        foreach (Collider collider in meleeWeaponCollider.HitColliders())
        {
            Debug.Log("HitEvent " + collider.gameObject.name);
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
            enemy.GetComponent<Rigidbody>().AddExplosionForce(hitForce, player.position, attackRange);

        if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
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
