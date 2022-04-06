using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Range : MonoBehaviour
{
    public GameObject weaponPrefab;
    private GameObject currentWeapon;

    private Animator animator;
    private int animIDisAttacking;
    private bool attacking;

    public bool canUse;

    private PlayerInputActions playerInputActions;

    private Weapon weapon;

    private Player player;
    [SerializeField] private Transform target;

    private PlayerStateManager stateManager;
    private float timeBetweenAttack;

    void Start()
    {
        player = GetComponent<Player>();
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        stateManager = GetComponent<PlayerStateManager>();

        animator = GetComponent<Animator>();
        animIDisAttacking = Animator.StringToHash("isAttacking");

        playerInputActions.Player.Fire.Enable();
        playerInputActions.Player.Fire.started += OnRangedAttack;

        SpawnWeapon();

        if (animator != null)
        {
            foreach (var item in animator.runtimeAnimatorController.animationClips)
            {
                if (item.name == "1H-RH@Attack01")
                {
                    timeBetweenAttack = item.length / animator.GetFloat("animSpeed");
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        playerInputActions.Player.Fire.started += OnRangedAttack;
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        currentWeapon = Instantiate(weaponPrefab, player.WeaponHand.position, Quaternion.identity);
        currentWeapon.transform.SetParent(player.WeaponHand);

        weapon = currentWeapon.GetComponent<Weapon>();
        weapon.parent = gameObject;
        weapon.target = target;
    }

    #region InputSystem

    private void OnRangedAttack(InputAction.CallbackContext obj)
    {
        if (!attacking)
        {
            attacking = true;

            stateManager.SwitchState(stateManager.attackState);

            if (animator != null)
                animator.SetBool(animIDisAttacking, attacking);

            Invoke("ResetAttack", timeBetweenAttack);
        }
    }

    public void HitEvent()
    {
        if(enabled)
            weapon.Shoot();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Fire.started -= OnRangedAttack;
    }

    #endregion

    private void OnDisable()
    {
        playerInputActions.Player.Fire.started -= OnRangedAttack;
        Destroy(currentWeapon);
    }

    private void ResetAttack()
    {
        attacking = false;
        stateManager.SwitchState(stateManager.moveState);
        if (animator != null)
            animator.SetBool(animIDisAttacking, false);
    }
}
