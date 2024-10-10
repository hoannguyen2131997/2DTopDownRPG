using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class TestPerforment : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    public TextMeshProUGUI TotalMemoryAllocatedText;
    public TextMeshProUGUI TotalMemoryInUseText;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount/ time);
            FpsText.text = frameRate.ToString() + " FPS";
            TotalMemoryAllocatedText.text = "Total Allocated Memory: " + Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024) + " MB";
            TotalMemoryInUseText.text = "Total Used Memory: " + Profiler.GetTotalReservedMemoryLong() / (1024 * 1024) + " MB";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
