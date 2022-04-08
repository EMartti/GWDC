using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDyingState : PlayerBaseState
{
    private Animator animator;
    private int animIDisDead;
    public NavMeshAgent agent;

    private PlayerInputActions inputActions;

    public override void EnterState(PlayerStateManager character)
    {
        animator = character.gameObject.GetComponent<Animator>();
        inputActions = PlayerInputs.Instance.playerInputActions;

        animIDisDead = Animator.StringToHash("isDead");
        if (animator != null)
            animator.SetBool(animIDisDead, true);

        inputActions.Player.Disable();
    }

    public override void UpdateState(PlayerStateManager character)
    {
        
    }
}
