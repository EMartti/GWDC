using UnityEngine;
using System;

public class PlayerAttackingState : PlayerBaseState
{
    public Transform target;
    public bool canTurnWhenAttacking = true;

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
            character.gameObject.transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
            //Debug.Log(character.gameObject.name + " is looking at " + target.gameObject.name + " at " + target.position);
        }
    }
}
