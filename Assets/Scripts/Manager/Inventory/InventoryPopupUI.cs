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
 
    private void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<EventsPlayerManager>();
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(ExitInventory);
        if(playerManager == null)
        {
            Debug.Log("Not find player");
        }

        playerManager.OnShowInventoryPopupPlayer += ShowInventoryPopupPlayer;
    }

    private void ShowInventoryPopupPlayer(object sender, OnShowInventoryPopupPlayerEventArgs e)
    {
        //Debug.Log("inventory: " + e.DataItemInventoryList.Count);
        for (int i = 0; i < e.DataItemInventoryList.Count; i++)
        {
            itemSlotsList[i].AddItem(e.DataItemInventoryList[i].itemName, e.DataItemInventoryList[i].itemQuantity, e.DataItemInventoryList[i].itemSprite);
        }
    }

    private void ExitInventory()
    {
        PopupManager.Instance.SetPopupInventory(false);
        playerManager.SetBlockControlPlayer(false);
    }
}
