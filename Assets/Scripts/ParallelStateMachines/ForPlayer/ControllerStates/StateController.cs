using UnityEngine;

public class StateController : MonoBehaviour
{
    public IMovementState currentMovementState;
    public IAttackState currentAttackState;

    [SerializeField] private PlayerData playerData;
    private PlayerInputHandler playerInputHandler;
    private Rigidbody2D rbPlayer;
    private Animator aniPlayer;

    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rbPlayer = GetComponent<Rigidbody2D>();
        aniPlayer = GetComponent<Animator>();

        bool isNotGetComponent = playerInputHandler == null || rbPlayer == null || aniPlayer == null;

        if(isNotGetComponent)
        {
            Debug.Log("Log Error - Component For Player is null");
        }
    }

    private void Start()
    {
       
        // Khởi tạo trạng thái ban đầu cho di chuyển và tấn công
        TransitionToMovementState(new IdleState());
        //TransitionToAttackState(new AutoAttackState());
    }

    private void Update()
    {
       
        //currentAttackState?.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentMovementState?.FixedUpdateState(this);
    }

    public void TransitionToMovementState(IMovementState newState)
    {
        currentMovementState?.ExitState(this);
        currentMovementState = newState;
        currentMovementState.EnterState(this);
    }

    //public void TransitionToAttackState(IAttackState newState)
    //{
    //    currentAttackState?.ExitState(this);
    //    currentAttackState = newState;
    //    currentAttackState.EnterState(this);
    //}

    public PlayerData GetPlayerData() => playerData;
    public PlayerInputHandler GetPlayerInputHandler() => playerInputHandler;
    public Rigidbody2D GetRbPlayer() => rbPlayer;
    public Animator GetAniPlayer() => aniPlayer;
}