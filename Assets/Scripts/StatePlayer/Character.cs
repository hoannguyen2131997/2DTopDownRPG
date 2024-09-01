using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public float playerSpeed = 5.0f;
    public bool FacingLeft { get { return facingLeft; } }
    private bool facingLeft = false;
    // Components to get
    [HideInInspector]
    public Rigidbody2D _rb;

    [HideInInspector]
    public Animator _animator;

    [HideInInspector]
    public Vector2 playerVelocity;

    
    public Transform weaponCollider;
    private SpriteRenderer spriteRenderer;
    
    // Values to use
    public StateMachine movementSM;
    public StandingState standing;
    public bool IsBlockAnimation;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);

        movementSM.Initialize(standing);
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    public Transform GetWeaponCollider() { return weaponCollider; }

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
