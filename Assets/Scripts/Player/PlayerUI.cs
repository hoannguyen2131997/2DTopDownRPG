using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void UpdateHealthSlider(int _maxHealth, int _currentHealth)
    {
        if(EventUIForPlayer.Instance.healthSliderPlayer != null)
        {
            EventUIForPlayer.Instance.healthSliderPlayer.maxValue = _maxHealth;
            EventUIForPlayer.Instance.healthSliderPlayer.value = _currentHealth;
        } else
        {
            Debug.Log("Error - Heal player not find");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(playerController != null)
        {
            playerController.HandleCollisionEnemy(collision);
        } else
        {
            Debug.Log("Error - PlayerController not find!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerController != null)
        {
            playerController.HandleEnterTriggerBullet(collider);
        }
        else
        {
            Debug.Log("Error - PlayerController not find!");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (playerController != null)
        {
            playerController.HandleExitTriggerBullet(collider);
        }
        else
        {
            Debug.Log("Error - PlayerController not find!");
        }
    }
}
