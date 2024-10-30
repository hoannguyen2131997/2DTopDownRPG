using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;
    private GameInputSystemSingleton gameInputSystemSingleton;

    protected override void Awake()
    {
        base.Awake();
        
        gameInputSystemSingleton = GameObject.Find("Player").GetComponent<GameInputSystemSingleton>();
        if(gameInputSystemSingleton == null)
        {
            Debug.Log("gameInputSystemSingleton is null");
        }
    }
    // action
    private void Start()
    {
        gameInputSystemSingleton.OnItemInventory += GameInput_OnToggleActiveHighlight;
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void GameInput_OnToggleActiveHighlight(object sender, GameInputSystemSingleton.OnItemInventoryPressedEventArgs e)
    {
        ToggleActiveHighlight(e.indexInventory - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        //ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        WeaponSlot inventorySlot = childTransform.GetComponentInChildren<WeaponSlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        
       GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,0);
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
