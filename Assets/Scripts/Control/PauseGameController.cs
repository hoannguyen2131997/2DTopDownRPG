using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField] private Button pauseGameBtn;
    [SerializeField] private Button playGameBtn;

    private EventsPlayerManager eventPlayerManager;

    private void Start()
    {
        pauseGameBtn.onClick.AddListener(OnPauseGame);
        playGameBtn.onClick.AddListener(OnPlayGame);
    }
    private void OnPauseGame()
    {
        Time.timeScale = 0;
        pauseGameBtn.gameObject.SetActive(false);
        playGameBtn.gameObject.SetActive(true);
        eventPlayerManager = GameObject.Find("EventsPlayerManager").GetComponent<EventsPlayerManager>();
        GameInputSystemSingleton.Instance.isBlockInput = true;
        if (eventPlayerManager != null )
        {
            eventPlayerManager.SetBlockControlPlayer(true);
        }
    }

    private void OnPlayGame()
    {
        Time.timeScale = 1;
        pauseGameBtn.gameObject.SetActive(true);
        playGameBtn.gameObject.SetActive(false);
        eventPlayerManager = GameObject.Find("EventsPlayerManager").GetComponent<EventsPlayerManager>();
        GameInputSystemSingleton.Instance.isBlockInput = false;
        if (eventPlayerManager != null)
        {
            eventPlayerManager.SetBlockControlPlayer(false);
        }
    }
}
