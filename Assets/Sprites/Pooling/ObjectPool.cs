using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public GameObject prefab; // Đối tượng cần pool (ví dụ: đạn)
    public int poolSize = 10; // Số lượng đối tượng trong pool
    [SerializeField] private Transform ListBulletCircleParent;
    private Queue<GameObject> poolObjects = new Queue<GameObject>(); // Hàng đợi lưu trữ các đối tượng trong pool
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
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // Tạo đối tượng mới
            obj.transform.parent = ListBulletCircleParent.transform;
            obj.tag = "EnemyBullet";
            obj.SetActive(false); // Ẩn đối tượng khi chưa sử dụng
            poolObjects.Enqueue(obj); // Thêm vào pool
        }
    }

    // Lấy đối tượng từ pool
    public GameObject GetFromPool()
    {
        if (poolObjects.Count > 0)
        {
            GameObject obj = poolObjects.Dequeue(); // Lấy đối tượng từ hàng đợi
            obj.SetActive(true); // Kích hoạt đối tượng
            return obj;
        }
        else
        {
            // Nếu pool rỗng, tạo thêm đối tượng mới
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // Trả đối tượng về pool
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // Ẩn đối tượng
        poolObjects.Enqueue(obj); // Thêm đối tượng vào lại hàng đợi
    }
}
