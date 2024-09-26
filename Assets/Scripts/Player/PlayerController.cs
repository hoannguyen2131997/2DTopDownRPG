using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private DataPlayerToSave playerToSave;
    [SerializeField] private GameObject playerUIPrefab;

    private GameInputSystemSingleton gameInputSystemSingleton;
    private EventsPlayerManager eventsPlayerManager;
    private InventoryDataPlayer inventoryDataPlayer;

    private PlayerUI playerUI;
    private Character character;

    public bool isDead { get; private set; }
    private int currentHealth;
    private bool canTakeDamege = true;
    private bool canTakeDamageBullet = true;
    private KnockBack knockBack;
    private Flash flash;
    readonly int DEATH_HASH = Animator.StringToHash("Death");
    const string TOWN_TEXT = "Scene1";
    private bool isBlockAttackPlayer;
    private Rigidbody2D rb;

    private void Awake()
    {
        flash = gameObject.GetComponentInChildren<Flash>();
        knockBack = gameObject.GetComponentInChildren<KnockBack>();
        playerUI = playerUIPrefab.GetComponent<PlayerUI>();
        character = playerUIPrefab.GetComponent<Character>();
        eventsPlayerManager = GetComponent<EventsPlayerManager>();
        gameInputSystemSingleton = GetComponent<GameInputSystemSingleton>();
        rb = GetComponentInChildren<Rigidbody2D>();
        inventoryDataPlayer = GetComponent<InventoryDataPlayer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        playerUI.UpdateHealthSlider(maxHealth, currentHealth);
        eventsPlayerManager.OnPlayerTakeDame += UpdateDataPlayerHealth;
        eventsPlayerManager.OnBlockControlPlayer += OnBlockControlPlayer;
    }

    private void OnBlockControlPlayer(object sender, OnBlockControlPlayerEventArgs e)
    {
        isBlockAttackPlayer = e.IsBlockControlPlayer;
        character.IsBlockAnimation = e.IsBlockControlPlayer;
    }

    private void LoadDataPlayer()
    {
        currentHealth = playerToSave.currentHeal;
        maxHealth = playerToSave.maxHeal;
    }

    private void SaveDataPlayer()
    {
        playerToSave.UpdateCurrentHeal(currentHealth);
        playerToSave.UpdateCurrentHeal(maxHealth);
    }

    private void UpdateDataPlayerHealth(object sender, OnTakeDamePressedEventArgs e)
    {
        if (currentHealth != 0 || maxHealth != 0)
        {
            currentHealth -= e.DamageTaken;
        }
        playerUI.UpdateHealthSlider(maxHealth, currentHealth);
    }

    public void CollisionDetected(EnemyIA enemyIA)
    {
        if (!canTakeDamege) { return; }

        Transform transform = enemyIA.gameObject.transform;
        ScreenSnackManager.Instance.ShakeSreen();
        knockBack.GetKnockedBack(gameObject.transform, transform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamege = false;
        StartCoroutine(DamageRecoveryRoutine());
        eventsPlayerManager.GetDamegePlayer(enemyIA.GetCollisionDamage());
    }

    public void HealPlayer(int _healItem)
    {
        currentHealth += _healItem;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        playerUI.UpdateHealthSlider(maxHealth,currentHealth);
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeadLoadSceneRoutine());
        }
    }

    private IEnumerator DeadLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamege = true;
    }

    public void HandleCollisionEnemy(Collision2D collision)
    {
        // Take damege
        EnemyIA enemyIA = collision.gameObject.GetComponent<EnemyIA>();

        if(enemyIA != null)
        {
            CollisionDetected(enemyIA);
        }

        // Get item
        Item item = collision.gameObject.GetComponent<Item>();
        if (inventoryDataPlayer != null && item != null)
        {
            inventoryDataPlayer.dataItemInventories.Add(collision.gameObject.GetComponent<Item>().GetItem());
            Destroy(collision.gameObject);
        }
    }

    public void HandleEnterTriggerBullet(Collider2D collider)
    {
        if (collider.CompareTag("EnemyBullet") && canTakeDamageBullet)
        {
            GrapeProjectile bullet = collider.GetComponent<GrapeProjectile>();

            if (bullet != null)
            {
                Debug.Log("Player bị trúng đạn từ enemy với damage: " + bullet.DamageToPlayer);
                eventsPlayerManager.GetDamegePlayer((int)bullet.DamageToPlayer);
                canTakeDamageBullet = false;
            }
        }

    }
    public void HandleExitTriggerBullet(Collider2D collider)
    {
        canTakeDamageBullet = true;
    }
}
