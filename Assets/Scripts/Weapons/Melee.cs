using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Melee : MonoBehaviour 
{  
    private PlayerInputActions playerInputActions;    
    private Animator animator;     

    private int animIDisAttacking;
    bool attacking;

    [SerializeField] private GameObject weaponPrefab;
    private GameObject currentWeapon;

    [HideInInspector] public WeaponMelee weapon;

    public bool canUse = true;    

    private Player player;

    private float timeBetweenAttack = 1f;

    private PlayerStateManager stateManager;

    private void Start() {

        player = GetComponent<Player>();
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        stateManager = GetComponent<PlayerStateManager>();

        animator = GetComponent<Animator>();
        animIDisAttacking = Animator.StringToHash("isAttacking");        

        playerInputActions.Player.Fire.Enable();
        playerInputActions.Player.Fire.started += OnMeleeAttack;

        SpawnWeapon();

        if (animator != null)
        {
            foreach (var item in animator.runtimeAnimatorController.animationClips)
            {
                if (item.name == "Attack")
                {
                    timeBetweenAttack = item.length / animator.GetFloat("animSpeed");
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        playerInputActions.Player.Fire.started += OnMeleeAttack;
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        currentWeapon = Instantiate(weaponPrefab, player.WeaponHand.position, player.WeaponHand.rotation);
        currentWeapon.transform.SetParent(player.WeaponHand);

        weapon = currentWeapon.GetComponent<WeaponMelee>();
    }

    #region InputSystem
    private void OnMeleeAttack(InputAction.CallbackContext obj) 
    {
        if (!attacking)
        {
            attacking = true;

            stateManager.SwitchState(stateManager.attackState);
            weapon.PlayAttackSound();

            if (animator != null)
                animator.SetBool(animIDisAttacking, attacking);

            Invoke("ResetAttack", timeBetweenAttack);
        }        
    }

    #endregion

    public void AttackStart() 
    {
        if(enabled)
            weapon.canDamage = true;
    }
    public void AttackEnd()
    {
        if (enabled)
            weapon.canDamage = false;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.started -= OnMeleeAttack;
        Destroy(currentWeapon);
    }

    private void ResetAttack() {
        attacking = false;
        stateManager.SwitchState(stateManager.moveState);
        if (animator != null)
            animator.SetBool(animIDisAttacking, false);
    }
}
