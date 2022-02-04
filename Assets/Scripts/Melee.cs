using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //public Rigidbody rb;
    public GameObject hitEffect;
    public LayerMask whatIsEnemies;
    private PlayerInputActions playerInputActions;
    AudioSource audioSource;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject weapon;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private bool automatic;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private Transform player;

    public Collider weaponCollider;

    public int hitDamage;
    public float attackRange;
    public float hitForce;

    public float maxLifeTime;

    int collisions;
    bool alreadyHitOnce = false;
    PhysicMaterial physics_mat;

    bool attacking, readyToAttack;

    private bool allowInvoke = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        readyToAttack = true;
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        playerInputActions.Player.Fire.Enable();
        playerInputActions.Player.Fire.started += OnFire;
    }

    #region InputSystem
    private void OnFire(InputAction.CallbackContext obj)
    {
        attacking = true;

        if (readyToAttack && attacking)
        {
            Attack();
        }
        attacking = false;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.Disable();
    }
    #endregion

    void Update()
    {
        attacking = false;
        if (automatic && playerInputActions.Player.Fire.ReadValue<float>() > 0)
            attacking = true;

        //Automatic Attacking
        if (readyToAttack && attacking && automatic)
        {
            Attack();
        }
    }

    public void Attack()
    {        
        readyToAttack = false;

        foreach(Collider collider in GetColliders())
        {
            if(collider.CompareTag("Enemy"))
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(hitDamage);
                    Hit(collider);
                }
            }            
        }        

        if (allowInvoke)
        {
            Invoke("ResetAttack", timeBetweenAttack);
            allowInvoke = false;
        }

        if(attackSound != null)
            audioSource.PlayOneShot(attackSound, 0.7F);
    }

    private void Hit(Collider enemy)
    {
        if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);

        Debug.Log("add force");
        if (enemy.GetComponent<Rigidbody>())
            enemy.GetComponent<Rigidbody>().AddExplosionForce(hitForce, player.position, attackRange);

        if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
    

    private void ResetAttack()
    {
        readyToAttack = true;
        allowInvoke = true;
    }
}
