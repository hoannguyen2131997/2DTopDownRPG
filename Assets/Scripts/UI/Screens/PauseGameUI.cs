using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button PlayBtn;

    private const string LoadSceneMode = "SneneMenu";
    private void Start()
    {
        MenuBtn.onClick.AddListener(ActiveMenuScene);
        PlayBtn.onClick.AddListener(BackGamePlay);
    }

    private void ActiveMenuScene()
    {
        Application.Quit();
    }

    private void BackGamePlay()
    {
        PopupManager.Instance.SetPauseGame(false);
        PopupManager.Instance.SetPopupPause(false);
    }
}
