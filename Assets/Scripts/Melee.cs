using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Melee : MonoBehaviour { 
    #region VariablesClasses

    [Serializable]
    public class AudioInspector
    {
        public AudioClip hitSound;
        public AudioClip attackSound;
    }

    [Serializable]
    public class VisualsInspector
    {
        public GameObject weapon;
        public GameObject hitEffect;
    }

    [Serializable] 
    public class ParametersInspector
    {
        public float timeBetweenAttack;
        public int hitDamage;
        public float hitForce;
        public int baseDamage = 50;
    }
    #endregion
    private AudioManager aM;
    private PlayerInputActions playerInputActions;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    public LayerMask whatIsEnemies;
    public Animator animator;
    public MeleeWeapon meleeWeaponCollider;
    AudioSource audioSource;   

    [SerializeField] private AudioInspector audio;
    [SerializeField] private VisualsInspector visuals;
    [SerializeField] public ParametersInspector parameters;

    private int animIDisAttacking;
    bool attacking, readyToAttack;
    private bool allowInvoke = true;
    private bool isPlayer = false;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();

        readyToAttack = true;

        if (gameObject.tag == "Player") isPlayer = true;

        animator = GetComponent<Animator>();
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
        }
        animIDisAttacking = Animator.StringToHash("isAttacking");

        if (animator != null)
        {
            foreach (var item in animator.runtimeAnimatorController.animationClips)
            {
                if (item.name == "1H-RH@Attack01")
                {
                    parameters.timeBetweenAttack = item.length;
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
            if (readyToAttack)
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
        attacking = true;

        if (animator != null)
        animator.SetBool(animIDisAttacking, attacking);


        if (allowInvoke) {
            Invoke("ResetAttack", parameters.timeBetweenAttack);
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
                damageable.TakeDamage(parameters.hitDamage, gameObject);
                Hit(collider);
            }
        }
    }

    private void Hit(Collider enemy) {
        if (visuals.hitEffect != null) Instantiate(visuals.hitEffect, transform.position, Quaternion.identity);

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
