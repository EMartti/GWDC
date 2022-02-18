using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DyingState : CharacterBaseState
{
    private Animator animator;
    private int animIDisDead;
    public NavMeshAgent agent;

    public override void EnterState(CharacterStateManager character)
    {
        animator = character.gameObject.GetComponent<Animator>();
        animIDisDead = Animator.StringToHash("isDead");
        animator.SetBool(animIDisDead, true);

        agent = character.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        
    }
}
