using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingState : CharacterBaseState
{
    public NavMeshAgent agent;
    private Transform target;
    private float meleeRange = 1.5f;
    private bool targetDead;
    private Animator animator;

    public override void EnterState(CharacterStateManager character)
    {
        //play roar sound or something like that, player is seen here
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = character.GetComponent<NavMeshAgent>();
        agent.isStopped = false;

        animator = character.gameObject.GetComponent<Animator>();
    }


    public override void UpdateState(CharacterStateManager character)
    {
        if (!targetDead)
        {
            Vector3 relativePos = target.position - character.transform.position;     
            Quaternion rotation = Quaternion.Lerp(Quaternion.LookRotation(character.transform.forward), Quaternion.LookRotation(relativePos, Vector3.up), 0.1f);
            character.transform.rotation = rotation;

            agent.destination = target.position;
            float dist = Vector3.Distance(character.transform.position, target.position);
            if (dist < meleeRange)
            {
                agent.isStopped = true;
                character.SwitchState(character.attackState);
            }
        }
        else
        {
            agent.isStopped = true;
            character.SwitchState(character.IdleState);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
