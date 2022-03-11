using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Objective[] objectives;

    public GameObject inputManagerPrefab;
    public GameObject audioManager;
    public GameObject playerStats;


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


