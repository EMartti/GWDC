using UnityEngine;
using System;

public class PlayerAttackingState : PlayerBaseState
{
    public Transform target;
    public bool canTurnWhenAttacking;
    public float turnRateWhenAttacking = 1f;

    private Player player;

    private PlayerStateManager character;

    private PlayerInputActions playerInputActions;
    public override void EnterState(PlayerStateManager character)
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        playerInputActions.Player.Move.Disable();
    }

    public override void UpdateState(PlayerStateManager character)
    {       
        if (canTurnWhenAttacking)
        {
            character.gameObject.transform.LookAt(target.position);
        }
    }
}
