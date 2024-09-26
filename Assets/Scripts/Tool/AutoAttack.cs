using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private float attackCooldown = 1f; // Thời gian hồi giữa các đòn tấn công
    private float lastAttackTime;

    private SwordPlayer swordPlayer;

    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController không được tìm thấy! Vô hiệu hóa script này.");
            this.enabled = false; // Vô hiệu hóa script nếu không tìm thấy
        }
    }

    private void Start()
    {
        // Get weapon
        swordPlayer = GameObject.FindObjectOfType<SwordPlayer>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Time.time - lastAttackTime > attackCooldown)
        {
            swordPlayer.Attack();
            lastAttackTime = Time.time;
        }
    }
}
