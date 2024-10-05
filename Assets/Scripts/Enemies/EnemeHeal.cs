using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemeHeal : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider damageBar;
    [SerializeField] private CanvasGroup healthCanvasGroup;
    [SerializeField] private float hideDelay = 3f;
    [SerializeField] private float damageBarDelay = 0.5f;  // Thời gian trễ trước khi damageBar giảm
    [SerializeField] private float damageBarSpeed = 1f;  // Tốc độ giảm damageBar

    public bool IsDead { get; private set; }
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;
    private float lerpSpeed = 0.05f;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    void Start()
    {
   
        // Gán giá trị khởi đầu cho thanh máu
        currentHealth = startingHealth;
        healthBar.maxValue = startingHealth;
        damageBar.maxValue = startingHealth;
        damageBar.value = currentHealth;
        healthBar.value = currentHealth;

        // Bắt đầu với thanh máu ẩn (alpha = 0)
        healthCanvasGroup.alpha = 0;
    }

    public void TakeDamage(int damage)
    {
        // Hiển thị thanh máu ngay lập tức và hủy bất kỳ fade nào đang chạy
        healthCanvasGroup.DOKill();  // Hủy bỏ các tween cũ để tránh xung đột
        healthCanvasGroup.DOFade(1, 0.5f);  // Hiển thị thanh máu trong 0.5 giây

        // Khi bị tấn công, hiển thị thanh máu và giảm máu
        currentHealth -= damage;
        healthBar.value = currentHealth;
        //knockBack.GetKnockedBack(Character.Instance.transform, knockBackThrust);

        DOTween.To(() => damageBar.value, x => damageBar.value = x, currentHealth, damageBarSpeed)
          .SetDelay(damageBarDelay);

        // Sau khi nhận sát thương, ẩn thanh máu sau 3 giây bằng tween
        healthCanvasGroup.DOFade(0, 0.5f).SetDelay(hideDelay);  // Ẩn trong 0.5 giây sau 3 giây
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            IsDead = true;
            this.gameObject.SetActive(false);
        }
    }
}
