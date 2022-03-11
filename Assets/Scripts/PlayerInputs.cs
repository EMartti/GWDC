using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    
    #region Singleton
    public static PlayerInputs Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PlayerInputs)) as PlayerInputs;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static PlayerInputs instance;
    #endregion

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
}
