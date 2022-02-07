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
        Health health = player.GetComponent<Health>();
        health.OnDeath += Health_OnDeath;
    }

    private void Health_OnDeath(object sender, EventArgs e)
    {
        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("GAME OVER!");
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }
}
