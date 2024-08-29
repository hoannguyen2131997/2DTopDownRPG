using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Attack state");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Attack state");
    }
}
