using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    private void Awake()
    {
        Application.targetFrameRate = 1000;
    }

    private void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("EndCrossFade");
        yield return new WaitForSeconds(transitionTime);
    }
}
