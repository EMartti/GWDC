using UnityEngine;

public class AttackingState : CharacterBaseState
{
    private Transform target;
    private float meleeRange = 1;
    private bool attacking = false;
    private float attackInterval = 1;
    private float timer;
    private Melee characterMelee;

    public override void EnterState(CharacterStateManager character)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        characterMelee = character.GetComponent<Melee>();
        characterMelee.HitEvent();
        attacking = true;
        timer = 0;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (timer >= attackInterval)
            character.SwitchState(character.MoveState);

        timer += Time.deltaTime;        
    }
}
