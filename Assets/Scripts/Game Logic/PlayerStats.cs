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

    // Pelaajaan liittyv�t statsit - S�ilyy skenejen v�lill�

    [SerializeField] 
    public int playerLevel;
    public float levelProgress = 0f;
    public int damageBonus;

}
