using UnityEngine; 

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

        input = character.GetEvent();
        
    }
    private Transform nearestEnemy;
    
    // Tìm kẻ thù gần nhất
    private void FindNearestEnemy()
    {
        IsAttack[] enemies = GameObject.FindObjectsOfType<IsAttack>();
        nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (IsAttack enemy in enemies)
        {
            //float distanceToEnemy = Vector3.Distance(character.gameObject.transform.position, enemy.transform.position);
            //if (distanceToEnemy < shortestDistance)
            //{
            //    shortestDistance = distanceToEnemy;
            //    nearestEnemy = enemy.transform;
            //}

            float distanceToEnemy = Vector3.Distance(character.gameObject.transform.position, enemy.transform.position);
            if (distanceToEnemy <= attackRange)
            {
                AttackEnemy();
                return;
            }
        }
    }
    public float attackCooldown = 1f; // Thời gian hồi giữa các đòn tấn công
    private float lastAttackTime;
    void AttackEnemy()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            var item = GameObject.FindObjectOfType<SwordPlayer>();
            //character.gameObject.transform.rotation = Quaternion.Euler(nearestEnemy.position - character.gameObject.transform.position).normalized;
            item.Attack();
            //Debug.Log("Tấn công kẻ thù: " + nearestEnemy.name);
            // Thêm logic tấn công ở đây (ví dụ: trừ máu kẻ thù)
            lastAttackTime = Time.time;
        }
    }
    public float raycastInterval = 0.5f; // Khoảng thời gian giữa mỗi lần Raycast
    private float raycastTimer;
    public float attackRange = 3f;
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!character.IsBlockAnimation)
        {
            character._animator.SetFloat(KeyPlayer._horizontal, input.x);
            character._animator.SetFloat(KeyPlayer._vertical, input.y);

            if (input != Vector2.zero)
            {
                character._animator.SetFloat(KeyPlayer._lastHorizontal, input.x);
                character._animator.SetFloat(KeyPlayer._lastVertical, input.y);
            }
        }

        //raycastTimer += Time.deltaTime;

        //// Nếu đã qua thời gian raycastInterval thì thực hiện Raycast
        //if (raycastTimer >= raycastInterval)
        //{
        //    FindNearestEnemy();
        //    raycastTimer = 0f; // Đặt lại bộ đếm
        //}


        //// Tấn công nếu kẻ thù nằm trong phạm vi
        //if (nearestEnemy != null)
        //{
        //    float distanceToEnemy = Vector3.Distance(character.gameObject.transform.position, nearestEnemy.position);
        //    if (distanceToEnemy <= attackRange)
        //    {
        //        AttackEnemy();
        //    }
        //}
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
        public const string _InputX = "InputX";
        public const string _InputY = "InputY";
    }
}
