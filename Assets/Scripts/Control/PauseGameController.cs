using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField] private Button pauseGameBtn;
    [SerializeField] private Button playGameBtn;

    private EventsPlayerManager eventPlayerManager;
    private bool isPauseGame;

    private void Start()
    {
        pauseGameBtn.onClick.AddListener(OnPauseGame);
        PopupManager.Instance.onPauseGame += SetOnPauseGameBtn;
    }

    private void SetOnPauseGameBtn(object sender, PopupManager.OnPauseGameEventArgs e)
    {
        isPauseGame = e.IsPauseGame;
        if(isPauseGame)
        {
            Time.timeScale = 0;
            pauseGameBtn.gameObject.SetActive(false);
            playGameBtn.gameObject.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            pauseGameBtn.gameObject.SetActive(true);
            playGameBtn.gameObject.SetActive(false);
        }
    }

    private void OnPauseGame()
    {
        PopupManager.Instance.SetPopupPause(true);
    }
}
