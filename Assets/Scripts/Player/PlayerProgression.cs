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

    public void GiveXp(float earnedXp)
    {
        playerStats.currentXp += earnedXp;

        // Debug
        Debug.Log("Player earned " + earnedXp.ToString() + "XP");

        if (playerStats.currentXp >= xpRequiredToLvlUp)
        { 
            LevelUp();
        }
    }

    public void LevelUp()
    {
            // Remove level xp from currentXp
            playerStats.currentXp -= xpRequiredToLvlUp;

            // Level / stat increase
            playerStats.playerLevel += 1;
            xpRequiredToLvlUp *= xpRequiredMultiplier;
                
            // Health increase
            playerHealthScript.maxHealth = playerHealthScript.maxHealth + maxHealthAddedPerLevel;
            playerHealthScript.currentHealth = playerHealthScript.maxHealth;
            
            // Damage increase
            PlayerStats.Instance.damageBonus += damageAddedPerLevel;
            meleeScript.parameters.hitDamage = meleeScript.parameters.baseDamage + PlayerStats.Instance.damageBonus;

            // Debug
            Debug.Log("Player achieved Level " + playerStats.playerLevel);


            // Jos edellisestä level-upista jäi ylimäärästä xp:tä, joka riittää toiseen leveliin - Level uppaa uudestaan
            if (playerStats.currentXp >= xpRequiredToLvlUp)
            {
                LevelUp();
            }
    }
}
