using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singleton<PlayerControl>
{
    [SerializeField] private float _moveSpeed = 5f;

    // Components to get
    private Rigidbody2D _rb;
    private Animator _animator;

    // Values to use
    private Vector2 _movement;

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void PlayerMovement()
    {
        _movement = GameInputSystemSingleton.Instance.GetMovementVectorNormalized();
        _animator.SetFloat(KeyPlayer._horizontal, _movement.x);
        _animator.SetFloat(KeyPlayer._vertical, _movement.y);
       
        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(KeyPlayer._lastHorizontal, _movement.x);
            _animator.SetFloat(KeyPlayer._lastVertical, _movement.y);
        }

        _rb.velocity = _movement * _moveSpeed;
    }

    private void Update()
    {
        PlayerMovement();
    }

    public class KeyPlayer
    {
        public const string _horizontal = "Horizontal";
        public const string _vertical = "Vertical";
        public const string _lastHorizontal = "LastHorizontal";
        public const string _lastVertical = "LastVertical";
    }
}
