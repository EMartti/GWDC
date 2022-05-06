using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{

    #region Singleton
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(Player)) as Player;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static Player instance;
    #endregion

    public PlayerPerks playerPerks;

    private Health health;

    private UIPerks uiPerks;

    private TextMeshProUGUI perkPointText;

    private PlayerProgression playerProgression;

    private bool canUseDash = false;

    private bool canUseDash2 = false;

    private PlayerInputActions playerInputActions;

    private PlayerController playerController;

    [SerializeField] private CharacterController characterController;
 
    private Dash dash;

    public Transform WeaponHand;

    public int damageBonus;

    // -------------- Ability cooldown --------------
    [SerializeField] private float dashCooldown = 3f;

    [SerializeField] private float dashLength = 0.1f;

    [SerializeField] private bool isAbilityOnCooldown = false; 

    public enum Abilities { Dash }
    private Abilities ability1;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        playerInputActions.Player.Ability1.Enable();
        playerInputActions.Player.Ability1.started += OnSpacebar;

        playerPerks = new PlayerPerks();
        playerPerks.OnPerkUnlocked += playerPerks_OnPerkUnlocked;

        playerProgression = GetComponent<PlayerProgression>();
        playerProgression.OnPpLevelUp += PlayerProgression_OnPpLevelUp;

        uiPerks = GameManager.Instance.GetComponent<UIPerks>();

        health = GetComponent<Health>();

        ability1 = Abilities.Dash;

        uiPerks.SetPlayerPerks(GetPlayerPerks());
    }

    private void Update()
    {
        // Check if player is grounded
        if (characterController.isGrounded)
        {
            canUseDash2 = true;
        }
        else
        {
            canUseDash2 = false;
        }
    }
    private void OnDestroy()
    {
        playerPerks.OnPerkUnlocked -= playerPerks_OnPerkUnlocked;
        playerProgression.OnXpLevelUp -= PlayerProgression_OnPpLevelUp;
    }

    #region InputSystem

    private void OnSpacebar(InputAction.CallbackContext obj)
    {
        switch (ability1)
        {
            case Abilities.Dash:
                if (!canUseDash || !canUseDash2 || isAbilityOnCooldown) break;
                OnDash();
                StartCoroutine(AbilityCooldown(dashCooldown));
                Invoke("EndDash", dashLength);
                break;
        }
    }

    private void OnDisable()
    {        
        playerInputActions.Player.Ability1.Disable();
    }

    #endregion

    #region Perks
    public void PlayerProgression_OnPpLevelUp(object sender, EventArgs e)
    {
        playerPerks.AddPerkPoints();
    }

    private void playerPerks_OnPerkUnlocked(object sender, PlayerPerks.OnPerkUnlockedEventArgs e)
    {
        switch (e.perkType)
        {
            case PlayerPerks.PerkType.MaxHP1:
                health.maxHealth += 100;
                health.currentHealth = health.maxHealth;
                break;

            case PlayerPerks.PerkType.MaxHP2:
                health.maxHealth += 100;
                health.currentHealth = health.maxHealth;
                break;

            case PlayerPerks.PerkType.MaxHP3:
                health.maxHealth += 100;
                health.currentHealth = health.maxHealth;
                break;

            case PlayerPerks.PerkType.Dash:
                canUseDash = true;
                break;

            case PlayerPerks.PerkType.DashDr:
                dashLength *= 2f;
                break;

            case PlayerPerks.PerkType.DashRedCool:
                dashCooldown /= 2f;
                break;
        }

    }

    public PlayerPerks GetPlayerPerks()
    {
        return playerPerks;
    }

    #endregion

    #region Abilities


    private class Ability
    {
        private Abilities type;
        private float cooldown;
    }

    // Ability cooldown timer
    private IEnumerator AbilityCooldown(float cooldownLength)
    {
        isAbilityOnCooldown = true;
        yield return new WaitForSeconds(cooldownLength);
        isAbilityOnCooldown = false;
    }


    // Enable dash effects
    public void OnDash()
    {    
        playerController.baseSpeed *= 5;
        playerController.dashing = true;
    }

    // Disable dash effects
    private void EndDash()
    {
        playerController.baseSpeed /= 5;
        playerController.dashing = false;
    }
    #endregion

}
