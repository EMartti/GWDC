using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingState : CharacterBaseState
{
    public NavMeshAgent agent;
    private Transform target;

    public override void EnterState(CharacterStateManager character)
    {
        //play roar sound or something like that, player is seen here
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = character.GetComponent<NavMeshAgent>();
    }


    public override void UpdateState(CharacterStateManager character)
    {
        character.transform.LookAt(target.position);
        agent.destination = target.position;
    }

    public override void AttackState(CharacterStateManager character)
    {

    }

    public override void MoveState(CharacterStateManager character)
    {

    }
}
