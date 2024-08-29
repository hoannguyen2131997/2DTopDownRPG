using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    const string HEALTH_SLIDER_TEXT = "HealSliderPlayer";
    private Slider healthSlider;

    public void UpdateHealthSlider(int _maxHealth, int _currentHealth)
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = _maxHealth;
        healthSlider.value = _currentHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyIA enemyIA = collision.gameObject.GetComponent<EnemyIA>();
        transform.parent.GetComponent<PlayerController>().CollisionDetected(enemyIA);
    }
}