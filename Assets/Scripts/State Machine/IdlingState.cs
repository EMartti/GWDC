using UnityEngine;

public class IdlingState : CharacterBaseState
{
    Transform target;
    public float aggroRange = 4;
    private bool targetDead;

    public override void EnterState(CharacterStateManager character)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (!targetDead)
        {
            float dist = Vector3.Distance(character.transform.position, target.position);
            if (dist < aggroRange)
            {
                character.SwitchState(character.aiMoveState);
            }
        }                   
    }
}
