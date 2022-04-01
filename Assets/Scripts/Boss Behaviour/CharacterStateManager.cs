using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    public CharacterBaseState currentState;
    public IdlingState idleState = new IdlingState();
    public MovingState moveState = new MovingState();
    public AttackingState attackState = new AttackingState();
    public DyingState deadState = new DyingState();

    public float aggroRange = 10f;
    public float attackInterval = 1f;
    public float attackRange = 1f;
    [SerializeField] private bool canTurnWhenAttacking = false;
    public float turnRateWhenAttacking = 1f;

    private void Start()
    {
        idleState.aggroRange = aggroRange;

        attackState.attackInterval = attackInterval;
        attackState.canTurnWhenAttacking = canTurnWhenAttacking;
        attackState.turnRateWhenAttacking = turnRateWhenAttacking;

        moveState.attackRange = attackRange;

        currentState = idleState;

        currentState.EnterState(this);

        Health.OnDeath += Health_OnDeath;

    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(CharacterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        //Debug.Log("switched state to: " + state);
    }

    public void Health_OnDeath(Health sender)
    {
        if (sender != null)
        {
            if (sender.gameObject == gameObject)
            {
                currentState = deadState;
                currentState.EnterState(this);
            }
        }
    }
    private void OnDestroy()
    {
        Health.OnDeath -= Health_OnDeath;
    }
}
