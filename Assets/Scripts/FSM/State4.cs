using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State4 : BaseState
{
    public override void EnterState(FSM_Player player)
    {
        player.SetState(player.States[3]);
    }

    public override void Update(FSM_Player player)
    {
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            player.TransitionToState(player.S2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            player.TransitionToState(player.S1);
        }
    }
}
