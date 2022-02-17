using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingState : CharacterBaseState
{
    public NavMeshAgent agent;
    private Transform target;
    private float meleeRange = 1;

    public override void EnterState(CharacterStateManager character)
    {
        //play roar sound or something like that, player is seen here
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = character.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }


    public override void UpdateState(CharacterStateManager character)
    {
        character.transform.LookAt(target.position);
        agent.destination = target.position;
        float dist = Vector3.Distance(character.transform.position, target.position);
        if (dist < meleeRange)
        {
            agent.isStopped = true;
            character.SwitchState(character.attackState);
        }
    }
}
