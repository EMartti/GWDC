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

    private PlayerInputActions playerInputActions;

    private PlayerController playerController;

    [SerializeField] private CharacterController characterController;
 
    private Dash dash;

    public Transform WeaponHand;

    public int damageBonus;

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

    #region InputSystem

    private void OnSpacebar(InputAction.CallbackContext obj)
    {
        switch (ability1)
        {
            case Abilities.Dash:
                if (!canUseDash) break;
                OnDash();
                Debug.Log("Dash");
                Invoke("EndDash", 0.1f);
                break;
        }
    }

    private void OnDisable()
    {        
        playerInputActions.Player.Ability1.Disable();
    }

    #endregion

    public void PlayerProgression_OnPpLevelUp(object sender, EventArgs e)
    {
        playerPerks.AddPerkPoints();
    }

    // Check if player is grounded
    // Used when executing Dash or other abilities
    private void Update()
    {
        if (characterController.isGrounded)
        {
            canUseDash = true;
        }
        else
        {
            canUseDash = false;
        }
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
        }

    }

    private void OnDestroy()
    {
        playerPerks.OnPerkUnlocked -= playerPerks_OnPerkUnlocked;
        playerProgression.OnXpLevelUp -= PlayerProgression_OnPpLevelUp;
    }


    public PlayerPerks GetPlayerPerks()
    {
        return playerPerks;
    }

    private class Ability
    {
        private Abilities type;
        private float cooldown;
    }

    public void OnDash()
    {    
        playerController.baseSpeed *= 5;
        playerController.dashing = true;
    }

    private void EndDash()
    {
        playerController.baseSpeed /= 5;
        playerController.dashing = false;
    }

}
