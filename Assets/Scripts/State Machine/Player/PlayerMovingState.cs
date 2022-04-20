using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovingState : PlayerBaseState
{
    private PlayerInputActions playerInputActions;
    private bool isGameWon = false;

    public override void EnterState(PlayerStateManager character)
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        isGameWon = GameManager.Instance.isGameWon;

        if (isGameWon)
            playerInputActions.Player.Move.Disable();
        else
            playerInputActions.Player.Move.Enable();
    }


    public override void UpdateState(PlayerStateManager character)
    {
        if (isGameWon)
        {
            playerInputActions.Player.Move.Disable();
        }
    }
}
