using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform player;

    private void Start()
    {
        //SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _virtualCamera.Follow = player;
        //if(_virtualCamera == null || Character.Instance.transform == null)
        //{
        //    Debug.LogError("null");
        //}
    }
}
