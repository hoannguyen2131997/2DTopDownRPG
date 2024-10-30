using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
//    public WeaponEntry[] weaponEntries; // Mảng chứa các vũ khí
//    private Dictionary<string, GameObject> weaponDictionary = new Dictionary<string, GameObject>();
//    private GameObject currentWeapon; // Vũ khí hiện tại của người chơi
//    public Transform weaponHolder; // Vị trí giữ vũ khí

//    private void Start()
//    {
//        //InitializeWeapons();
//        EquipWeapon("sword"); // Ví dụ: khởi tạo với vũ khí "sword"
//    }

//    // Khởi tạo vũ khí và lưu vào Dictionary dựa trên ID
//    private void InitializeWeapons()
//    {
//        foreach (WeaponEntry entry in weaponEntries)
//        {
//            GameObject weaponInstance = Instantiate(entry.weaponPrefab, weaponHolder);
//            weaponInstance.SetActive(false); // Tắt vũ khí khi khởi tạo
//            weaponDictionary.Add(entry.weaponID, weaponInstance); // Lưu vào Dictionary với key là ID
//        }
//    }

//    // Trang bị vũ khí dựa trên ID định danh
//    public void EquipWeapon(string weaponID)
//    {
//        if (currentWeapon != null)
//        {
//            currentWeapon.SetActive(false); // Tắt vũ khí hiện tại
//        }

//        if (weaponDictionary.ContainsKey(weaponID))
//        {
//            currentWeapon = weaponDictionary[weaponID]; // Lấy vũ khí từ Dictionary
//            currentWeapon.SetActive(true); // Kích hoạt vũ khí mới
//            Debug.Log("Equipped weapon: " + weaponID);
//        }
//        else
//        {
//            Debug.LogWarning("Weapon with ID " + weaponID + " not found.");
//        }
//    }

    //public void PerformAttack()
    //{
    //    if (currentWeapon != null)
    //    {
    //        Weapon weapon = currentWeapon.GetComponent<Weapon>();
    //        if (weapon != null)
    //        {
    //            weapon.Attack();
    //        }
    //    }
    //}
}

[System.Serializable]
public class WeaponEntry
{
    public string weaponID; // ID định danh cho vũ khí
    public GameObject weaponPrefab; // Prefab của vũ khí
}