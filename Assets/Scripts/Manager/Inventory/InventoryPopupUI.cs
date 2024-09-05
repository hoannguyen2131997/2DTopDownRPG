using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopupUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    public List<ItemSlot> itemSlotsList;

    private bool menuActivated;
    private EventsPlayerManager playerManager;
    private InventoryController inventoryController;
    private PlayerController playerController;
    private List<DataItemInventory> dataItemInventories = new List<DataItemInventory>();

    private void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<EventsPlayerManager>();
        inventoryController = GameObject.Find("Player").GetComponent<InventoryController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(ExitInventory);
        if(playerManager == null || inventoryController == null)
        {
            Debug.Log("Not find player");
        }

        playerManager.OnShowInventoryPopupPlayer += ShowInventoryPopupPlayer;
    }

    private void ShowInventoryPopupPlayer(object sender, OnShowInventoryPopupPlayerEventArgs e)
    {
        PopupManager.Instance.SetPauseGame(true);
        playerManager.SetBlockControlPlayer(true);
        dataItemInventories = e.DataItemInventoryList;
        Debug.Log("inventory: " + dataItemInventories.Count);
        for (int i = 0; i < dataItemInventories.Count; i++)
        {
            itemSlotsList[i].AddItem(dataItemInventories[i].itemName, dataItemInventories[i].itemQuantity, dataItemInventories[i].itemSprite);
        }
    }

    private void ExitInventory()
    {
        PopupManager.Instance.SetPopupInventory(false);
        PopupManager.Instance.SetPauseGame(false);
        playerManager.SetBlockControlPlayer(false);
    }
}
