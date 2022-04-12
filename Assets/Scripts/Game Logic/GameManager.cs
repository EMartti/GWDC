using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static GameManager instance;
    #endregion

    public Objective[] objectives;

    public GameObject inputManagerPrefab;
    public GameObject audioManager;
    public GameObject playerStats;

    public GameObject winCanvas;


    public GameObject sword;
    public GameObject bow;
    public GameObject hammer;

    public bool canPlayerAttack = true;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class Objective : MonoBehaviour
{
    public abstract bool IsAchieved();
    public abstract void Complete();
    public abstract void DrawHUD();
}


