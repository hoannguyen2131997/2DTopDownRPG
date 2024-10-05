using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawObjectAddressable : MonoBehaviour
{
    [SerializeField] private AssetLabelReference map;
    [SerializeField] private Transform mapNormal;

    private void Start()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(map);
        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
      if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject mapObj = Instantiate(asyncOperationHandle.Result);
            mapObj.transform.SetParent(mapNormal);
            mapObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.Log("Failed to load!");
        }
    }
}
