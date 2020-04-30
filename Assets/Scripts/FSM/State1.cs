using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : BaseState
{
    public override void EnterState(FSM_Player player)
    {
        player.SetState(player.States[0]);
    }

    public override void Update(FSM_Player player)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.TransitionToState(player.S1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.TransitionToState(player.S2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.TransitionToState(player.S3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.SetState(player.States[4]);
        }
    }
}
