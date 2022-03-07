using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static PlayerStats instance;
    #endregion

    // Pelaajaan liittyvät statsit - Säilyy skenejen välillä

    [SerializeField] public int playerLevel;
    [SerializeField] public int damageBonus;
    [SerializeField] public int healthBonus;
    [SerializeField] public float currentXp = 0f;
    [SerializeField] public float xpRequiredToLvlUp = 1000f;

}
