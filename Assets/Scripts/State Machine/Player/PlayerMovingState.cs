using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovingState : PlayerBaseState
{
    private PlayerInputActions playerInputActions;

    public override void EnterState(PlayerStateManager character)
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        playerInputActions.Player.Move.Enable();
    }


    public override void UpdateState(PlayerStateManager character)
    {
        
    }
}
