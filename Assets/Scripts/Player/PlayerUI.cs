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
            Debug.Log("Heal player not find");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(playerController != null)
        {
            playerController.HandleCollisionPlayerUI(collision);
        } else
        {
            Debug.Log("PlayerController not find!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerController != null)
        {
            playerController.HandleEnterTriggerPlayerUI(collider);
        }
        else
        {
            Debug.Log("PlayerController not find!");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (playerController != null)
        {
            playerController.HandleExitTriggerPlayerUI(collider);
        }
        else
        {
            Debug.Log("PlayerController not find!");
        }
    }
}
