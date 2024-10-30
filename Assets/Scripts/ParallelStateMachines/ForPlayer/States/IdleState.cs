using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IMovementState
{
    public void EnterState(StateController controller)
    {
        Debug.Log("Entering Idle State");
    }

    public void FixedUpdateState(StateController controller)
    {
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    controller.TransitionToMovementState(new WalkState());
        //}
        if(controller.GetPlayerInputHandler().GetInputPlayer().x != 0 || controller.GetPlayerInputHandler().GetInputPlayer().y != 0)
        {
            controller.TransitionToMovementState(new WalkState());
        }
    }

    public void ExitState(StateController controller)
    {
        Debug.Log("Exiting Idle State");
    }
}
