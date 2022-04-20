using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public event EventHandler OnGameOver;
    [SerializeField] private GameObject gameOverCanvas;
    private PlayerInputActions playerInputActions;
    private GameObject player;
    void Start()
    {
        gameOverCanvas.SetActive(false);

        player = Player.Instance.gameObject;

        Health.OnDeath += Health_OnDeath;

        playerInputActions = PlayerInputs.Instance.playerInputActions;
    }

    private void Health_OnDeath(Health sender)
    {
        if(sender.gameObject == player) EndGame();
    }

    private void EndGame()
    {
        gameOverCanvas.SetActive(true);
        Debug.Log("GAME OVER!");
        if(OnGameOver != null) OnGameOver.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.Disable();

    }

    private void OnDestroy()
    {
        Health.OnDeath -= Health_OnDeath;
    }
}
