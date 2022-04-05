using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

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

    private PlayerPerks playerPerks;

    private Health health;

    private UIPerks uiPerks;

    private PlayerProgression playerProgression;

    private bool canUseDash = false;

    private PlayerInputActions playerInputActions;

    private Dash dash;

    public Transform WeaponHand;

    public int damageBonus;

    public enum Abilities { Dash }
    private Abilities ability1;

    private void Start()
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        playerInputActions.Player.Ability1.Enable();
        playerInputActions.Player.Ability1.started += UseAbility1;

        playerPerks = new PlayerPerks();
        playerPerks.OnPerkUnlocked += playerPerks_OnPerkUnlocked;

        playerProgression = GetComponent<PlayerProgression>();
        playerProgression.OnLevelUp += PlayerProgression_OnLevelUp;

        uiPerks = GameObject.Find("GameManager").GetComponent<UIPerks>();

        health = GetComponent<Health>();

        ability1 = Abilities.Dash;

        uiPerks.SetPlayerPerks(GetPlayerPerks());
    }

    #region InputSystem

    private void UseAbility1(InputAction.CallbackContext obj)
    {
        switch (ability1)
        {
            case Abilities.Dash:
                if (!canUseDash) break;
                dash.OnDash();
                Invoke("EndDash", 0.1f);
                break;
        }
    }

    private void OnDisable()
    {        
        playerInputActions.Player.Ability1.Disable();
    }

    #endregion

    public void PlayerProgression_OnLevelUp(object sender, EventArgs e)
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
        }

    }

    private void OnDestroy()
    {
        playerPerks.OnPerkUnlocked -= playerPerks_OnPerkUnlocked;
        playerProgression.OnLevelUp -= PlayerProgression_OnLevelUp;
    }

    private void EndDash()
    {
        dash.End();
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

}
