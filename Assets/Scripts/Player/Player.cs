using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerPerks playerPerks;

    private Health health;

    private UIPerks uiPerks;

    private void Awake()
    {


        playerPerks = new PlayerPerks();
        playerPerks.OnPerkUnlocked += playerPerks_OnPerkUnlocked;

        uiPerks = GameObject.Find("GameManager").GetComponent<UIPerks>();

        health = GetComponent<Health>();
    }

    private void Start()
    {
        uiPerks.SetPlayerPerks(GetPlayerPerks());
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
        }

    }

    public PlayerPerks GetPlayerPerks()
    {
        return playerPerks;
    }

}
