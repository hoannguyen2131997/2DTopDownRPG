using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float idItem;
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;

    public DataItemInventory GetItem()
    {
        DataItemInventory dataItemInventory = new DataItemInventory(idItem, itemName, quantity, sprite);
        return dataItemInventory;
    }
}
