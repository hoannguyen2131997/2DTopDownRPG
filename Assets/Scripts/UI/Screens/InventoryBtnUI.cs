using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBtnUI : MonoBehaviour
{
    private Button inventoryBtn;
    private InventoryController inventoryController;

    private void Awake()
    {
        inventoryBtn = GetComponent<Button>();
        inventoryController = GameObject.Find("Player").GetComponent<InventoryController>();
        if (inventoryController != null)
        {
            Debug.Log("Find player Inventory");
        } else
        {
            Debug.Log("Not find player Inventory");
        }
    }

    private void Start()
    {
        inventoryBtn.onClick.AddListener(inventoryController.ShowPopupInventoryUI);
        //eventPlayerControl.OnUpdateInventoryPlayer += 
    }

    private void ShowPopupInventory()
    {
        //popupInventory.SetActive(true);
        //PopupManager.Instance.SetPauseGame(true);
        //eventPlayerControl.SetBlockControlPlayer(true);
    }
}
