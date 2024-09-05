using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private GameObject popupInventoryPrefab;
    public event EventHandler<OnPauseGameEventArgs> onPauseGame;
    protected void Awake()
    {
        base.Awake();
        
    }

    public void SetPopupPause(bool _onPopup)
    {
        popupPrefab.SetActive(_onPopup);
    }

    public void SetPauseGame(bool isPauseGame)
    {
        onPauseGame?.Invoke(this, new OnPauseGameEventArgs { IsPauseGame = isPauseGame });

        // if offline
        EventsPlayerManager eventsPlayerManager = GameObject.Find("Player").GetComponent<EventsPlayerManager>();
        if(eventsPlayerManager != null)
        {
            eventsPlayerManager.SetBlockControlPlayer(isPauseGame);
        }
    }

    public void SetPopupInventory(bool _onInventory)
    {
        popupInventoryPrefab.SetActive(_onInventory);
    }

    public class OnPauseGameEventArgs : EventArgs
    {
        public bool IsPauseGame;
    }
}
