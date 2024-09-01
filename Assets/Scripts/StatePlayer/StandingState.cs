using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Windows;

public class StandingState : State
{
    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter standing state");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = GameInputSystemSingleton.Instance.GetMovementVectorNormalized();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(!character.IsBlockAnimation)
        {
            character._animator.SetFloat(KeyPlayer._horizontal, input.x);
            character._animator.SetFloat(KeyPlayer._vertical, input.y);

            if (input != Vector2.zero)
            {
                character._animator.SetFloat(KeyPlayer._lastHorizontal, input.x);
                character._animator.SetFloat(KeyPlayer._lastVertical, input.y);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        character._rb.velocity = input * character.playerSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit standing state");
    }

    public class KeyPlayer
    {
        public const string _horizontal = "Horizontal";
        public const string _vertical = "Vertical";
        public const string _lastHorizontal = "LastHorizontal";
        public const string _lastVertical = "LastVertical";
    }
}
