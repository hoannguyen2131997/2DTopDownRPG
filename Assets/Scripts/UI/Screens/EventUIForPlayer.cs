using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventUIForPlayer : Singleton<EventUIForPlayer>
{
    public TMP_Text GoldText;
    public Button inventoryBtn;

    protected override void Awake()
    {
        base.Awake();
       
    }

    private void Start()
    {
        inventoryBtn.onClick.AddListener(ShowInventory);
    }

    private void ShowInventory()
    {
        PopupManager.Instance.SetPopupInventory(true);
    }
}
