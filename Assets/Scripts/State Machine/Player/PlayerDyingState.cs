using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDyingState : PlayerBaseState
{
    private Animator animator;
    private int animIDisDead;
    public NavMeshAgent agent;

    public override void EnterState(PlayerStateManager character)
    {
        animator = character.gameObject.GetComponent<Animator>();
        animIDisDead = Animator.StringToHash("isDead");
        if (animator != null)
            animator.SetBool(animIDisDead, true);

        agent = character.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    public override void UpdateState(PlayerStateManager character)
    {
        
    }
}
