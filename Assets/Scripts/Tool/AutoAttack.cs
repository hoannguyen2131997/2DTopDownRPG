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

    void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy") && Time.time - lastAttackTime > attackCooldown)
        {
            // Nếu SwordPlayer chưa được gán, tìm nó
            if (swordPlayer == null)
            {
                swordPlayer = GameObject.FindObjectOfType<SwordPlayer>();
                if (swordPlayer == null)
                {
                    Debug.LogWarning("SwordPlayer vẫn chưa được tạo!");
                    return; // Không làm gì nếu SwordPlayer chưa được tạo
                }
            }

            swordPlayer.Attack();
            lastAttackTime = Time.time;
        }
    }
}
