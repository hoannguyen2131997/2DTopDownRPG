using UnityEngine;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector2 velocity;
    protected Vector2 input;
    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
        Debug.Log("Enter State: " + this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
