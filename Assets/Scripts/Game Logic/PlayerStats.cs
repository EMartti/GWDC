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

    // Playerprogression tallentaa ja noutaa t‰‰lt‰ pelaajan statseihin liittyv‰t tiedot

    [SerializeField] 
    public int playerLevel;



}
