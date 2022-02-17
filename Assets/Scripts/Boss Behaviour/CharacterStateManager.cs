using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    CharacterBaseState currentState;
    public IdlingState IdleState = new IdlingState();
    public MovingState MoveState = new MovingState();
    public AttackingState attackState = new AttackingState();

    private void Start()
    {
        currentState = IdleState;

        currentState.EnterState(this);

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
}
