using UnityEngine;

public class IdlingState : CharacterBaseState
{
    Transform playerPos;
    float aggroRange = 4;

    public override void EnterState(CharacterStateManager character)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        float dist = Vector3.Distance(character.transform.position, playerPos.position);
        if (dist < aggroRange)
        {
            character.SwitchState(character.MoveState);
        }
            
    }

    public override void AttackState(CharacterStateManager character)
    {

    }

    public override void MoveState(CharacterStateManager character) 
    {
        
    }
}
