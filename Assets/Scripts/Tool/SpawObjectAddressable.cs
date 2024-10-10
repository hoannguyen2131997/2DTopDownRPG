using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawObjectAddressable : MonoBehaviour
{
    [ReadOnly]
    public string mapLabel; // Nhãn chung của các map

    [SerializeField] private AssetReference objToLoad;
    [SerializeField] private Transform parentTransform;

    private AsyncOperationHandle<GameObject> handle;

    public void LoadObjectAddressable()
    {
        handle = Addressables.LoadAssetAsync<GameObject>(objToLoad);
        handle.Completed += AsyncOperationHandle_Completed;
    }

    public void UnLoadAddressable()
    {
        Addressables.Release(handle);
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
      if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject obj = Instantiate(asyncOperationHandle.Result);
            obj.transform.SetParent(parentTransform);
            obj.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.Log("Failed to load!");
        }
    }
}
