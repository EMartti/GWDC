using UnityEngine;

public class AttackingState : CharacterBaseState
{
    private Transform target;
    public float attackInterval = 1;
    private float timer;
    private Melee characterMelee;

    public override void EnterState(CharacterStateManager character)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //animator = character.gameObject.GetComponent<Animator>();
        characterMelee = character.gameObject.GetComponent<Melee>();
        characterMelee.Attack();
        timer = 0;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (timer >= attackInterval)
            character.SwitchState(character.MoveState);

        timer += Time.deltaTime;        
    }
}
