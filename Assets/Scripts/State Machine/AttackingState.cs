using UnityEngine;
using System;

public class AttackingState : CharacterBaseState
{
    public Transform target;
    public float attackInterval = 1;
    private float timer;
    public bool canTurnWhenAttacking;
    public float turnRateWhenAttacking = 1f;

    public event EventHandler OnAttack;

    public override void EnterState(CharacterStateManager character)
    {
        if (OnAttack != null) OnAttack.Invoke(this, EventArgs.Empty);
        timer = 0;
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (timer >= attackInterval)
            character.SwitchState(character.aiMoveState);

        if (canTurnWhenAttacking)
        {
            Vector3 direction = target.position - character.gameObject.transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            character.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        timer += Time.deltaTime;        
    }
}
