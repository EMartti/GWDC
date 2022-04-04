using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager character);
    public abstract void UpdateState(PlayerStateManager character);
}
