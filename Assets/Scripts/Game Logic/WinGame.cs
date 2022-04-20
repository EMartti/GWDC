using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public event EventHandler OnWinGame;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private bool isTrigger = false;

    private PlayerInputActions playerInputActions;

    private SimpleObjective objective;

    void Start()
    {

        gameManager = GameManager.Instance;
        gameManager.winCanvas.SetActive(false);
        playerInputActions = PlayerInputs.Instance.playerInputActions;


        objective = GetComponent<SimpleObjective>();
        objective.OnObjectiveComplete += Win;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isTrigger)
        {
            if(OnWinGame != null) OnWinGame.Invoke(this, EventArgs.Empty);
            Debug.Log("GAME WON!");
            playerInputActions.Player.Disable();
            gameManager.winCanvas.SetActive(true);
        }        
    }

    public void Win(object sender, EventArgs e)
    {
        if (OnWinGame != null) OnWinGame.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.Disable();
        Debug.Log("GAME WON!");
        gameManager.winCanvas.SetActive(true);
        gameManager.isGameWon = true;
    }
}
