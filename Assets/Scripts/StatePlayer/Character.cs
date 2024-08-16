using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float playerSpeed = 5.0f;

    // Components to get
    [HideInInspector]
    public Rigidbody2D _rb;

    [HideInInspector]
    public Animator _animator;

    [HideInInspector]
    public Vector2 playerVelocity;

    // Values to use
    private Vector2 _movement;
    public StateMachine movementSM;
    public StandingState standing;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);

        movementSM.Initialize(standing);
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }
}
