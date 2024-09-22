using UnityEngine;

public class MapCulling : MonoBehaviour
{
    public Camera mainCamera;     // Camera chính
    public float padding = 2f;    // Khoảng đệm (padding)

    private SpriteRenderer[] childRenderers; // Mảng chứa các Renderer của các đối tượng con
 
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Tự động lấy camera chính nếu chưa chỉ định
        }

        // Lấy tất cả các Renderer của các đối tượng con
        childRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        CullingChildObjects();
    }

    void CullingChildObjects()
    {
        // Xác định giới hạn hiển thị của camera (view bounds)
        Vector3 camMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 camMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Thêm padding để mở rộng phạm vi camera
        camMin -= new Vector3(padding, padding, 0);
        camMax += new Vector3(padding, padding, 0);

        // Loop qua tất cả các Renderer của các đối tượng con
        foreach (Renderer rend in childRenderers)
        {
            if(rend != null)
            {
                Vector3 objPos = rend.transform.position;

                // Kiểm tra nếu đối tượng nằm trong phạm vi camera
                if (objPos.x > camMin.x && objPos.x < camMax.x &&
                    objPos.y > camMin.y && objPos.y < camMax.y)
                {
                    if (!rend.enabled)
                    {
                        rend.enabled = true;  // Bật renderer nếu đối tượng nằm trong phạm vi camera
                    }
                }
                else
                {
                    if (rend.enabled)
                    {
                        rend.enabled = false; // Tắt renderer nếu đối tượng nằm ngoài phạm vi camera
                    }
                }
            }
        }
    }
}
