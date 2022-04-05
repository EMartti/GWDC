using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerMovingState moveState = new PlayerMovingState();
    public PlayerAttackingState attackState = new PlayerAttackingState();
    public PlayerDyingState deadState = new PlayerDyingState();

    [SerializeField] private bool canTurnWhenAttacking = false;
    public Transform target;

    private void Start()
    {
        attackState.canTurnWhenAttacking = canTurnWhenAttacking;
        attackState.target = target;

        currentState = moveState;

        currentState.EnterState(this);

        Health.OnDeath += Health_OnDeath;

    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
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
