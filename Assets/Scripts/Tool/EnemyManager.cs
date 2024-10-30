using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [ReadOnly]
    [SerializeField] private float cameraBuffer = 6f;  // Vùng đệm tùy chỉnh
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float checkInterval = 1f;  // Kiểm tra mỗi 1 giây
    [SerializeField] private float nextCheckTime = 0f;

    private Camera mainCamera;
    public GameObject[] enemies;  // Mảng các kẻ địch con
    private int countListEnemy;
    private Coroutine checkEnemiesCoroutine;

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
         
    public IEnumerator CreateEnemiesList()
    {
        mainCamera = Camera.main;

        // Lấy tất cả các đối tượng con trong EnemyManager và lưu chúng vào mảng enemies
        countListEnemy = enemySpawner.enemyCount;
        enemies = new GameObject[countListEnemy];

        // Bắt đầu spawn và đợi cho đến khi hoàn tất
        yield return StartCoroutine(enemySpawner.SpawnEnemiesGradually());

        // Bây giờ quá trình spawn đã hoàn tất, bạn có thể bắt đầu kiểm tra
        checkEnemiesCoroutine = StartCoroutine(CheckEnemiesRoutine());
    }

    private IEnumerator CheckEnemiesRoutine()
    {
        while (true)
        {
            // Thực hiện kiểm tra enemy
            CheckEnemiesInCameraView();
            yield return new WaitForSeconds(1f); // Đợi một khoảng thời gian trước khi lặp lại
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
            if(enemeHeal.IsDead) continue;
           
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

    // Hàm để dừng Coroutine
    public void StopCheckEnemiesRoutine()
    {
        if (checkEnemiesCoroutine != null)
        {
            StopCoroutine(checkEnemiesCoroutine);
            checkEnemiesCoroutine = null;
        }
    }

    // Đảm bảo Coroutine dừng lại khi đối tượng bị hủy
    private void OnDestroy()
    {
        StopCheckEnemiesRoutine();
    }
}
