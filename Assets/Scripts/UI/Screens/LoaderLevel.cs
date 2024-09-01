using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderLevel : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public Button yourButton;

    public void LoadNextLevel()
    {
        int IndexScreenToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(IndexScreenToLoad));
    }

    IEnumerator LoadLevel(int indexScreenToLoad)
    {
        transition.SetTrigger("StartCrossFade");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(indexScreenToLoad);
    }

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        LoadNextLevel();
    }
}
