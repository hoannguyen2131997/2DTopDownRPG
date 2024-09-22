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
    [SerializeField] private int rangeCheckAttackableObjects = 2;
   
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
    private Tilemap map;

    public int GetCollisionDamage()
    {
        return collisionDamage;
    }
    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        map = MapData.Instance.GetMap();
       
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        CheckForAttackableObjects();
        MovementStateControl();
    }

    private void CheckForAttackableObjects()
    {
        Vector3Int enemyCellPosition = map.WorldToCell(this.transform.position);
        //Debug.Log("enemyCellPosition :" + enemyCellPosition.x + " " + enemyCellPosition.y);

        for (int x = - rangeCheckAttackableObjects; x <= rangeCheckAttackableObjects; x++)
        {
            for (int y = -rangeCheckAttackableObjects; y <= rangeCheckAttackableObjects; y++)
            {
                Vector3Int neighborCell = enemyCellPosition + new Vector3Int(x, y, 0);

                //Kiểm tra nếu có đối tượng hoặc người chơi trong ô này
                GameObject obj = CheckObjectInCell(neighborCell);
                if (obj != null)
                {
                    // Thực hiện hành động khi tìm thấy đối tượng (như người chơi)
                }
            }
        }
    }

    private GameObject CheckObjectInCell(Vector3Int cellPosition)
    {
        // Chuyển đổi vị trí ô lưới sang tọa độ thế giới (world position)
        Vector3 worldPosition = map.CellToWorld(cellPosition);

        // Kích thước ô lưới
        Vector2 cellSize = map.cellSize;

        // Kiểm tra đối tượng có collider trong ô lưới đó
        Collider2D[] colliders = Physics2D.OverlapBoxAll(worldPosition, cellSize, 0);
        foreach (Collider2D collider in colliders)
        {
            // Kiểm tra nếu đối tượng này có tag là "Player"
            if (collider.CompareTag("Player"))
            {
                return collider.gameObject;  // Trả về đối tượng người chơi
            }
        }
        return null;  // Không có đối tượng trong ô lưới
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

        if(Vector2.Distance(transform.position, Character.Instance.transform.position) < attackRange) {
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, Character.Instance.transform.position) > attackRange)
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
