using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{
    [SerializeField] private GameObject loadingCanvas; // Canvas màn hình chờ
    [SerializeField] private Image progressBar; // Thanh tiến trình

    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowLoadingScreen()
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(true);
            if (progressBar != null)
            {
                progressBar.fillAmount = 0f; // Đặt thanh tiến trình về 0
            }
        }
    }

    public void HideLoadingScreen()
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }
    }

    public void UpdateProgressBar(float progress)
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = progress;
        }
    }

    public IEnumerator LoadWithProgress(AsyncOperationHandle handle)
    {
        ShowLoadingScreen();
        while (!handle.IsDone)
        {
            UpdateProgressBar(handle.PercentComplete);
            Debug.Log("handle.PercentComplete : " + handle.PercentComplete);
            yield return null;
        }
        HideLoadingScreen();
    }
}
