using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public event EventHandler OnGameOver;
    [SerializeField] private GameObject gameOverCanvas;

    private GameObject player;
    void Start()
    {
        gameOverCanvas.SetActive(false);

        player = GameObject.FindWithTag("Player");

        Health.OnDeath += Health_OnDeath;
    }

    private void Health_OnDeath(Health sender)
    {
        if(sender.gameObject == player) EndGame();
    }

    private void EndGame()
    {
        gameOverCanvas.SetActive(true);
        Debug.Log("GAME OVER!");
        OnGameOver?.Invoke(this, EventArgs.Empty);       

    }
}
