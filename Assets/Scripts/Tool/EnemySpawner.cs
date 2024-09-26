using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;  // Prefab của enemy
    public int enemyCount = 5;      // Số lượng enemy cần tạo

    [Header("Spawn Area Settings")]
    public Vector2 spawnAreaMin;    // Góc dưới trái của vùng spawn (tọa độ trong world space)
    public Vector2 spawnAreaMax;    // Góc trên phải của vùng spawn (tọa độ trong world space)

    public void SpawnEnemies()
    {
       
        // Sinh ra các enemy ngẫu nhiên trong vùng spawn
        for (int i = 0; i < enemyCount; i++)
        {
            // Tạo vị trí ngẫu nhiên trong khoảng giới hạn spawn area
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // Tạo enemy tại vị trí ngẫu nhiên
            EnemyManager.Instance.enemies[i] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyManager.Instance.enemies[i].transform.SetParent(this.transform);   
        }
    }

    // Để kiểm tra trực quan vùng spawn trong Scene view
    private void OnDrawGizmosSelected()
    {
        // Vẽ một khung hình chữ nhật đại diện cho vùng spawn
        Gizmos.color = Color.red;
        Vector3 areaSize = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 0);
        Vector3 areaCenter = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, (spawnAreaMin.y + spawnAreaMax.y) / 2, 0);
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}
