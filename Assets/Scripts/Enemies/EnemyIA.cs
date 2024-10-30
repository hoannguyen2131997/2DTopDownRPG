using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private int collisionDamage = 10;

    private enum State
    {
        Roaming,
        Attacking
    }

    private bool canAttack = true;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private State state;
    private EnemyPathFinding enemyPathFinding;
    private GameObject _player;

    public int GetCollisionDamage()
    {
        return collisionDamage;
    }
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
                case State.Roaming: Roaming(); break;
                case State.Attacking: Attacking(); break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathFinding.MoveTo(roamPosition);

        if(_player == null)
        {
            _player = GameObject.FindWithTag("Player");
        }

        if (Vector2.Distance(transform.position, _player.transform.position) < attackRange) {
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player");
        }

        if (Vector2.Distance(transform.position, _player.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if(attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if(stopMovingWhileAttacking)
            {
                enemyPathFinding.StopMoving();
            } else
            {
                enemyPathFinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
