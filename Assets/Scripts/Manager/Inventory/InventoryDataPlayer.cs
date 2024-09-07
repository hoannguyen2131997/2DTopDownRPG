using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryDataPlayer : MonoBehaviour
{
    public List<DataItemInventory> dataItemInventories = new List<DataItemInventory>();

    private EventsPlayerManager eventsPlayerManager;
    private InventoryPopupUI inventoryPopupUI;

    private void Awake()
    {
        eventsPlayerManager = GetComponent<EventsPlayerManager>();
    }

    public void GetDataItemInventoryToShow()
    {
        eventsPlayerManager.GetDataListItemInventory(dataItemInventories);
    }
}

public class DataItemInventory
{
    public float idItem;
    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;

    public DataItemInventory(float _idItem, string _itemName, int _itemQuantity, Sprite _itemSprite)
    {
        idItem = _idItem;
        itemName = _itemName;
        itemQuantity = _itemQuantity;
        itemSprite = _itemSprite;
    }

    public void DeleteItem()
    {
        itemName = "";
        itemQuantity = 0;
        itemSprite = null;
    }

    public DataItemInventory GetInfoItem(float _idItem)
    {
        if (idItem == _idItem)
        {
            DataItemInventory dataItemInventory = new DataItemInventory(idItem, itemName, itemQuantity, itemSprite);
            return dataItemInventory;
        }

        return null; 
    }

    public bool IsEmptyItem()
    {
        bool result = false;

        if(idItem == -1 || itemName == "" || itemQuantity == 0 || itemSprite == null)

        {
            result = true;
        }

        return result;
    }
}
