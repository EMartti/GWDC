using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    [SerializeField] private float levelProgress = 0f;
    [SerializeField] private float xpRequiredToLvlUp = 1000f;
    [SerializeField] private int playerStartLevel;

    private PlayerStats playerStats;

    // Kerroin, jolla Level-uppiin tarvittava xp-määrä nousee jokaisen level-upin jälkeens
    [SerializeField] private float xpRequiredMultiplier = 1.5f;


    private void Start()
    {
        playerStats = PlayerStats.Instance;
    }


    void LevelUp()
    {
        //Increase player-level
        playerStats.playerLevel += 1;
        // Reset level-progress
        levelProgress = 0f;
        // Multiply seuraavaan level-uppiin tarvittava xp
        xpRequiredToLvlUp = xpRequiredToLvlUp * xpRequiredMultiplier;
        //Debug
        Debug.Log("Player achieved Level " + playerStats.playerLevel);
    }

    // Funktio antaa pelaajalle XP:tä
    // Callaa kaikista XP:n antajista (esim. XP-orbit, loot-chestit, etc.)
    // Toistaiseksi vain coin-dropit antaa xp
    public void GiveXp(float earnedXp)
    {
        levelProgress += earnedXp;

        // DEBUG
        Debug.Log("Player earned " + earnedXp.ToString() + "XP");

        if ( levelProgress >= xpRequiredToLvlUp)
        {
            LevelUp();
        }
    }
}
