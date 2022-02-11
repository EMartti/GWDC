using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public event EventHandler OnWinGame;
    [SerializeField] private GameObject winCanvas;

    private PlayerInputActions inputs;

    void Start()
    {
        winCanvas.SetActive(false);
        inputs = PlayerInputs.Instance.playerInputActions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(OnWinGame != null) OnWinGame.Invoke(this, EventArgs.Empty);
            inputs.Player.Disable();
            winCanvas.SetActive(true);
        }        
    }
}
