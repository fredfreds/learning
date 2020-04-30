using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State2 : BaseState
{
    public override void EnterState(FSM_Player player)
    {
        player.SetState(player.States[1]);
    }

    public override void Update(FSM_Player player)
    {
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            player.TransitionToState(player.S1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.TransitionToState(player.S4);
        }
    }
}
