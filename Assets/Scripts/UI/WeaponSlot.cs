using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    private void Start()
    {
       
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo; 
    }
}
