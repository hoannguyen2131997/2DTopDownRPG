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

        float currentProgress = 0f;
        float targetProgress = 0f;
        float progressSpeed = 0.2f; // Tốc độ điều chỉnh thanh tiến trình

        while (!handle.IsDone)
        {
            targetProgress = handle.PercentComplete; // Tiến trình thực tế
            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, progressSpeed * Time.deltaTime);
            UpdateProgressBar(currentProgress);
            yield return null;
        }

        // Đảm bảo thanh tiến trình đạt 100% một cách mượt mà
        while (currentProgress < .2f)
        {
            currentProgress = Mathf.MoveTowards(currentProgress, .2f, progressSpeed * Time.deltaTime);
            UpdateProgressBar(currentProgress);


            yield return null;
        }

        yield return EnemyManager.Instance.CreateEnemiesList();
      
        currentProgress = Mathf.MoveTowards(currentProgress, 1f, progressSpeed * Time.deltaTime);
        UpdateProgressBar(currentProgress);
        yield return null;

        HideLoadingScreen();
    }
}
