//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Player : Singleton<Player>
//{
//    public bool FacingLeft { get {  return facingLeft; } }
//    [SerializeField] private float moveSpeed = 1f;
//    [SerializeField] private float dashSpeed = 4f;
//    [SerializeField] private TrailRenderer trailRenderer;
//    [SerializeField] private Transform weaponCollider;

//    private Vector2 movement;
//    private Rigidbody2D rb;

//    private Animator animator;
//    private SpriteRenderer spriteRenderer;
//    private KnockBack knockBack;
//    private float startingMoveSpeed;

//    private bool facingLeft = false;
//    private bool isDashing = false;

//    protected override void Awake()
//    {
//        base.Awake();

//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        knockBack = GetComponent<KnockBack>();
//    }

//    private void Start()
//    {
//        GameInputSystemSingleton.Instance.OnDashAction += GameInput_OnDashAction;
//        startingMoveSpeed = moveSpeed;
//        ActiveInventory.Instance.EquipStartingWeapon();
//    }

//    public Transform GetWeaponCollider() { return weaponCollider; }

//    private void GameInput_OnDashAction(object sender, EventArgs e)
//    {
//        Dash();
//    }

//    private void Update()
//    {
//        //PlayerInput();
//    }

//    private void FixedUpdate()
//    {
//        //Move();
//        //AdjustPlayerFacingDirection();
//    }

//    private void PlayerInput()
//    {
//        //movement = GameInputSystemSingleton.Instance.GetMovementVectorNormalized();

//        //animator.SetFloat("MoveX", movement.x);
//        //animator.SetFloat("MoveY", movement.y);
//    }

//    //private void Move()
//    //{
//    //    if (knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead)
//    //    {
//    //        return;
//    //    }

//    //    rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
//    //}

//    private void AdjustPlayerFacingDirection()
//    {
//        Vector3 mousePos = Input.mousePosition;
//        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

//        if(mousePos.x < playerScreenPoint.x) 
//        {
//            spriteRenderer.flipX = true;
//            facingLeft = true;
//        } 
//        else
//        {
//            spriteRenderer.flipX = false;
//            facingLeft = false;
//        }
//    }

//    public void Dash()
//    {
//        if (!isDashing && Stamina.Instance.CurrentStamina > 0) 
//        {
//            Stamina.Instance.UseStamina();
//            isDashing = true;
//            moveSpeed *= dashSpeed;
//            trailRenderer.emitting = true;
//            StartCoroutine(EndDashRoutine());
//        }
//    }

//    private IEnumerator EndDashRoutine()
//    {
//        float dashTime = .2f;
//        float dashCD = .25f;
//        yield return new WaitForSeconds(dashTime);
//        moveSpeed = startingMoveSpeed;
//        trailRenderer.emitting = false;
//        yield return new WaitForSeconds(dashCD);
//        isDashing = false;
//    }
//}
