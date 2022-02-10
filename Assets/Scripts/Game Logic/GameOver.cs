using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public event EventHandler OnGameOver;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Health.OnDeath += Health_OnDeath;
    }

    private void Health_OnDeath(Health sender)
    {
        if(sender.gameObject == player) EndGame();
    }

    private void EndGame()
    {
        Debug.Log("GAME OVER!");
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }
}
