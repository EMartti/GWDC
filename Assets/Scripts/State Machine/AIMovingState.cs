using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingState : CharacterBaseState
{
    public NavMeshAgent agent;
    private Transform target;
    public float attackRange;
    private Health targetHealth;
    private Animator animator;

    public override void EnterState(CharacterStateManager character)
    {
        //play roar sound or something like that, player is seen here
        target = Player.Instance.gameObject.transform;

        agent = character.GetComponent<NavMeshAgent>();
        agent.isStopped = false;

        animator = character.gameObject.GetComponent<Animator>();

        targetHealth = target.gameObject.GetComponent<Health>();
    }


    public override void UpdateState(CharacterStateManager character)
    {
        if (!targetHealth.isDead)
        {
            agent.destination = target.position;
            float dist = Vector3.Distance(character.transform.position, target.position);
            if (dist < attackRange)
            {
                agent.isStopped = true;
                character.SwitchState(character.attackState);
            }
        }
        else
        {
            agent.isStopped = true;
            character.SwitchState(character.idleState);
        }

        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
