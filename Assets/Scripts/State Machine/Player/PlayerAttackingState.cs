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
            Vector3 direction = target.position - character.gameObject.transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            character.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
