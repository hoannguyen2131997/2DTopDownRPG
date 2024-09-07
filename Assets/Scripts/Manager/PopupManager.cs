using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    // Support Player to get data form Screen Popup for single player game
    // Multiplayer games, we can handle asynchronously here

    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private GameObject popupInventoryPrefab;
    public event EventHandler<OnPauseGameEventArgs> onPauseGame;

    private bool checkGetDataPlayer;
    private EventsPlayerManager eventsPlayerManager;
    private InventoryDataPlayer inventoryController;

    protected void Awake()
    {
        base.Awake();

        // if offline
        eventsPlayerManager = GameObject.Find("Player").GetComponent<EventsPlayerManager>();
        inventoryController = GameObject.Find("Player").GetComponent<InventoryDataPlayer>();
        if (eventsPlayerManager == null || inventoryController == null)
        {
            Debug.Log("Not find player!");
        }
    }

    public void SetPopupPause(bool _onPopup)
    {
        SetPauseGame(_onPopup); 
        popupPrefab.SetActive(_onPopup);
    }

    public void SetPopupInventory(bool _onInventory)
    {
        // Get data
        if(_onInventory)
        {
            inventoryController.GetDataItemInventoryToShow();
        }

        // Pause game
        SetPauseGame(_onInventory);

        // Show UI
        popupInventoryPrefab.SetActive(_onInventory);
    }

    public void SetPauseGame(bool isPauseGame)
    {
        onPauseGame?.Invoke(this, new OnPauseGameEventArgs { IsPauseGame = isPauseGame });

        if (eventsPlayerManager != null)
        {
            eventsPlayerManager.SetBlockControlPlayer(isPauseGame);
        }
    }

    public class OnPauseGameEventArgs : EventArgs
    {
        public bool IsPauseGame;
    }
}
