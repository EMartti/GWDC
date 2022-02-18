using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    CharacterBaseState currentState;
    public IdlingState IdleState = new IdlingState();
    public MovingState MoveState = new MovingState();
    public AttackingState attackState = new AttackingState();
    public DyingState deadState = new DyingState();


    private void Start()
    {
        currentState = IdleState;

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
