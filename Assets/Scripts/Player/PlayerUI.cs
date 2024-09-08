using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public void UpdateHealthSlider(int _maxHealth, int _currentHealth)
    {
        if(EventUIForPlayer.Instance.healthSliderPlayer != null)
        {
            EventUIForPlayer.Instance.healthSliderPlayer.maxValue = _maxHealth;
            EventUIForPlayer.Instance.healthSliderPlayer.value = _currentHealth;
        } else
        {
            Debug.Log("Heal player not find");
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyIA enemyIA = collision.gameObject.GetComponent<EnemyIA>();
        transform.parent.GetComponent<PlayerController>().CollisionDetected(enemyIA);

        InventoryDataPlayer inventoryController = transform.parent.GetComponent<InventoryDataPlayer>();
        if (inventoryController != null && collision.gameObject.GetComponent<Item>() != null)
        {
            inventoryController.dataItemInventories.Add(collision.gameObject.GetComponent<Item>().GetItem());
            Destroy(collision.gameObject);
        }
    }
}
