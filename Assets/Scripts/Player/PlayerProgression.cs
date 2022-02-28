using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Health playerHealthScript;
    [SerializeField] private Melee meleeScript;

    [Header("Level & XP")]
    [SerializeField] private float xpRequiredMultiplier = 1.5f;
    [SerializeField] private int playerStartLevel;
    [SerializeField] private float xpRequiredToLvlUp = 1000f;

    [Header("Health Settings")]
    [SerializeField] private int maxHealthAddedPerLevel;

    [Header("Damage")]
    [SerializeField] private int damageAddedPerLevel = 10;

   

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        playerHealthScript = GameObject.FindWithTag("Player").GetComponent<Health>();

        meleeScript = GameObject.FindWithTag("Player").GetComponent<Melee>();
    }


    void LevelUp()
    {
        // Increase player-level
        playerStats.playerLevel += 1;
        // Reset level-progress
        playerStats.levelProgress = 0f;
        // Multiply seuraavaan level-uppiin tarvittava xp
        xpRequiredToLvlUp = xpRequiredToLvlUp * xpRequiredMultiplier;

        // Nostaa pelaajan max healthia
        playerHealthScript.maxHealth = playerHealthScript.maxHealth + maxHealthAddedPerLevel;
        // Health refillaa level-upissa
        playerHealthScript.currentHealth = playerHealthScript.maxHealth;

        // Increase player damage
        meleeScript.parameters.hitDamage += damageAddedPerLevel;

        // Debug
        Debug.Log("Player achieved Level " + playerStats.playerLevel);
    }

    // Antaa pelaajalle XP:tä
    // Callaa kaikista XP:n antajista (esim. XP-orbit, loot-chestit, etc.)
    // Toistaiseksi vain coin-dropit antaa xp
    public void GiveXp(float earnedXp)
    {
        playerStats.levelProgress += earnedXp;

        // DEBUG
        Debug.Log("Player earned " + earnedXp.ToString() + "XP");

        if (playerStats.levelProgress >= xpRequiredToLvlUp)
        {
            LevelUp();
        }
    }
}
