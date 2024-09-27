using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private float cameraBuffer = 6f;  // Vùng đệm tùy chỉnh
    private Camera mainCamera;
    public GameObject[] enemies;  // Mảng các kẻ địch con
    [SerializeField] private EnemySpawner enemySpawner;

    private int countListEnemy;

    private float checkInterval = 1f;  // Kiểm tra mỗi 1 giây
    private float nextCheckTime = 0f;
    protected override void Awake()
    {
        base.Awake();

                
    }

    public void GetEnemyList(GameObject[] _enemy)
    {
        int count = _enemy.Length;

        for (int i = 0; i < count; i++)
        {
            enemies[i] = _enemy[i];
        }
    }
         
    private void Start()
    {
        mainCamera = Camera.main;

        // Lấy tất cả các đối tượng con trong EnemyManager và lưu chúng vào mảng enemies
        countListEnemy = enemySpawner.enemyCount;
        enemies = new GameObject[countListEnemy];
        enemySpawner.SpawnEnemies();
    }

    private void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            CheckEnemiesInCameraView();
            nextCheckTime = Time.time + checkInterval;
        }
    }

    private void CheckEnemiesInCameraView()
    {
        // Lấy vị trí và kích thước của camera
        Vector3 camPosition = mainCamera.transform.position;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // Tính toán vùng giới hạn của camera + vùng đệm
        float leftBound = camPosition.x - camWidth / 2 - cameraBuffer;
        float rightBound = camPosition.x + camWidth / 2 + cameraBuffer;
        float bottomBound = camPosition.y - camHeight / 2 - cameraBuffer;
        float topBound = camPosition.y + camHeight / 2 + cameraBuffer;

        // Duyệt qua mảng kẻ địch và bật/tắt dựa trên vị trí của chúng
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;
            EnemeHeal enemeHeal = enemy.GetComponent<EnemeHeal>();
            if(enemeHeal.IsDead)
            {
                continue;
            }
            // Nếu kẻ địch nằm trong vùng camera + vùng đệm thì kích hoạt, nếu không thì tắt
            if (enemyPosition.x > leftBound && enemyPosition.x < rightBound &&
                enemyPosition.y > bottomBound && enemyPosition.y < topBound)
            {
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(true);  // Kích hoạt nếu đang bị tắt
                }
            }
            else
            {
                if (enemy.activeSelf)
                {
                    enemy.SetActive(false);  // Tắt nếu đang được kích hoạt
                }
            }
        }
    }
}
