using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(FSM_Player player);

    public abstract void Update(FSM_Player player);
}
