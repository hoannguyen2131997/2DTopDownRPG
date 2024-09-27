using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public GameObject prefab; // Đối tượng cần pool (ví dụ: đạn)
    public int poolSize = 10; // Số lượng đối tượng trong pool
    [SerializeField] private Transform ListBulletCircleParent;
    private GameObject[] poolObjects; // Mảng lưu trữ các đối tượng trong pool
    private int currentIndex = 0;     // Biến theo dõi chỉ số của đối tượng được lấy

#if UNITY_EDITOR
    [SerializeField]
    private bool showGizmos;  // Hiển thị trên Inspector

    public bool ShowGizmos
    {
        get { return showGizmos; }
        private set { showGizmos = value; }
    }
#endif

    protected override void Awake()
    {
        base.Awake();
    }

    // Khởi tạo pool khi game bắt đầu
    void Start()
    {
        poolObjects = new GameObject[poolSize]; // Khởi tạo mảng
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // Tạo đối tượng mới
            obj.transform.parent = ListBulletCircleParent.transform;
            obj.tag = "EnemyBullet";
            obj.SetActive(false); // Ẩn đối tượng khi chưa sử dụng
            poolObjects[i] = obj; // Thêm đối tượng vào mảng
        }
    }

    // Lấy đối tượng từ pool
    public GameObject GetFromPool()
    {
        if(currentIndex == poolSize - 1)
        {
            currentIndex = 0;
        }

        // Duyệt qua mảng để tìm đối tượng không được kích hoạt
        for (int i = 0; i < poolSize; i++)
        {
            // Tính toán chỉ số của đối tượng hiện tại (để vòng lặp)
            int index = (currentIndex + i) % poolSize;

            // Nếu đối tượng không được kích hoạt (inactive)
            if (!poolObjects[index].activeInHierarchy)
            {
                poolObjects[index].SetActive(true); // Kích hoạt đối tượng
                currentIndex = (index + 1) % poolSize; // Cập nhật chỉ số hiện tại
                return poolObjects[index]; // Trả về đối tượng đã kích hoạt
            }
        }

        // Nếu tất cả các đối tượng trong pool đều đang được sử dụng, có thể mở rộng logic tại đây.
        Debug.LogWarning("All objects in pool are currently in use!");
        return null;
    }

    // Trả đối tượng về pool
    public void ReturnToPool(GameObject obj)
    {
       
        GrapeProjectile grapeProjectile = obj.GetComponent<GrapeProjectile>();
        obj.GetComponent<SpriteRenderer>().sprite = null;
        // Lấy tất cả CircleCollider2D trên GameObject

        if (grapeProjectile != null)
        {
            Destroy(grapeProjectile);
        }


        obj.SetActive(false); // Ẩn đối tượng
        // Đối tượng đã trở lại pool, nhưng không cần thay đổi chỉ số currentIndex.
    }
}
