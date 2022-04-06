using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Magic : MonoBehaviour {

    [SerializeField] private GameObject weaponPrefab;
    private GameObject currentWeapon;

    private Animator animator;
    private int animIDisAttacking;
    private bool attacking = false;

    private PlayerInputActions playerInputActions;

    private WeaponMagic weapon;

    public bool canUse = true;

    private Player player;
    [SerializeField] private Transform target;

    private PlayerStateManager stateManager;
    private float timeBetweenAttack;

    private void Start() {

        player = Player.Instance;

        player = GetComponent<Player>();
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        stateManager = GetComponent<PlayerStateManager>();

        animator = GetComponent<Animator>();
        animIDisAttacking = Animator.StringToHash("isAttacking");

        playerInputActions.Player.Fire.Enable();
        playerInputActions.Player.Fire.started += OnMageAttack;

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
        playerInputActions.Player.Fire.started += OnMageAttack;
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        currentWeapon = Instantiate(weaponPrefab, player.WeaponHand.position, Quaternion.identity);
        currentWeapon.transform.SetParent(player.WeaponHand);

        weapon = currentWeapon.GetComponent<WeaponMagic>();
        weapon.parent = gameObject;
        weapon.target = target;
    }

    private void OnMageAttack(InputAction.CallbackContext obj)
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
        if (enabled)
            weapon.Attack();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.started -= OnMageAttack;
    }

    private void ResetAttack() {
        attacking = false;
        stateManager.SwitchState(stateManager.moveState);
        if (animator != null)
            animator.SetBool(animIDisAttacking, false);
    }
}
