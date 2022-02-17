using UnityEngine;

public abstract class CharacterBaseState
{
    public abstract void EnterState(CharacterStateManager character);
    public abstract void UpdateState(CharacterStateManager character);

}
