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

    private PlayerUI playerUI;
    private Character character;

    public bool isDead { get; private set; }
    private int currentHealth;
    private bool canTakeDamege = true;
    private KnockBack knockBack;
    private Flash flash;
    readonly int DEATH_HASH = Animator.StringToHash("Death");
    const string TOWN_TEXT = "Scene1";
    private bool isBlockAttackPlayer;

    private void Awake()
    {
        flash = gameObject.GetComponentInChildren<Flash>();
        knockBack = gameObject.GetComponentInChildren<KnockBack>();
        playerUI = playerUIPrefab.GetComponent<PlayerUI>();
        character = playerUIPrefab.GetComponent<Character>();
        eventsPlayerManager = GetComponent<EventsPlayerManager>();
        gameInputSystemSingleton = GetComponent<GameInputSystemSingleton>();
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

        if (enemyIA)
        {
            Transform transform = enemyIA.gameObject.transform;
            ScreenSnackManager.Instance.ShakeSreen();
            knockBack.GetKnockedBack(gameObject.transform, transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
            canTakeDamege = false;
            StartCoroutine(DamageRecoveryRoutine());
            //CheckIfPlayerDeath();
            eventsPlayerManager.GetDamegePlayer(enemyIA.GetDamegeEneme());
        }
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
}
