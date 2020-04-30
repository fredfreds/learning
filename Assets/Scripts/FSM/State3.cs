using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : BaseState
{
    public override void EnterState(FSM_Player player)
    {
        player.SetState(player.States[2]);
    }

    public override void Update(FSM_Player player)
    {
        if(Input.GetKeyUp(KeyCode.Alpha3))
        {
            player.TransitionToState(player.S1);
        }
    }
}
