using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MapManager : MonoBehaviour
{
    [ReadOnly]
    public string mapLabel; // Nhãn chung của các map

    [SerializeField] private Transform parentTransform;
    private AsyncOperationHandle<GameObject> currentMapHandle; // Handle của map hiện tại
    private GameObject currentMapInstance; // Instance của map hiện tại


    public void LoadObjectAddressable(AssetReference mapAssetReference)
    {
        // Unload map hiện tại trước khi tải map mới
        UnloadCurrentMap();

        // Hiển thị màn hình chờ
        // ShowLoadingScreen();

        // Bắt đầu tải map mới với màn hình chờ
        AsyncOperationHandle<GameObject> handle = mapAssetReference.InstantiateAsync();

        StartCoroutine(LoadingManager.Instance.LoadWithProgress(handle));
        handle.Completed += AsyncOperationHandle_Completed;
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            currentMapInstance = asyncOperationHandle.Result;
            currentMapInstance.transform.SetParent(parentTransform);
            currentMapInstance.transform.localPosition = Vector3.zero;

            // Lưu handle của map mới để sử dụng khi cần unload
            currentMapHandle = asyncOperationHandle;
        }
        else
        {
            Debug.Log("Failed to load the map!");
        }
    }

    private void UnloadCurrentMap()
    {
        // Kiểm tra xem instance của map hiện tại có tồn tại không
        if (currentMapInstance != null)
        {
            // Giải phóng instance và hủy đối tượng trong scene
            Addressables.ReleaseInstance(currentMapHandle);
            currentMapInstance = null;
        }
    }
}
