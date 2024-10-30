using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordController : MonoBehaviour
{
    // Input
    [SerializeField] private PlayerInputHandler playerInputHandler;

    // Ref Object
    [SerializeField] private GameObject _flashAttackWeapon;
    [SerializeField] private GameObject _colliderAttackWeapon;
    [SerializeField] private SwordUI _swordNormal;

    // Data
    [SerializeField] private InfoWeaponTransform _infoWeaponTransform; // rotation for dir and collder weapon
    [SerializeField] private WeaponPlayerData _WeaponPlayerData;
  
    [SerializeField] private float validToChangeDirWeapon = 0.1f;

    // To Test
    [SerializeField] private float attackSpeedChange = 1f;
    [SerializeField] private float attackSpeed = 1f;

    private float lastAttackTime;
    private Vector2 inputPlayer;
    private DamageCalculator _damageCalculator;
    private SlashAnim _splashAnim;

    private void Awake()
    {
        _damageCalculator = new DamageCalculator();
        _splashAnim = _flashAttackWeapon.GetComponent<SlashAnim>();
    }

    private void FixedUpdate()
    {
        inputPlayer = playerInputHandler.GetInputPlayer();
       
        if(inputPlayer != Vector2.zero)
        {
            RotateWeapon(inputPlayer);
        }
    }

    public void Attack(Collider2D collision)
    {
        float speedWeapon = _WeaponPlayerData.attackSpeed;
        float speedWeaponIncreate = attackSpeedChange;
        attackSpeed = (1 / attackSpeedChange) + speedWeapon;

        if (collision.CompareTag("Enemy") && Time.time - lastAttackTime > attackSpeed)
        {
            _swordNormal.TriggerAttack(inputPlayer);
            lastAttackTime = Time.time;
        }
    }

    public void RotateWeapon(Vector2 inputPlayer)
    {
        string temp = CheckDirectionWeapon(inputPlayer);
        _splashAnim.SetAniFlash(temp);

        if (temp == DirectionWeaponContanst.UpWeapon)
        {
            _swordNormal.RotateWeapon(DirectionWeaponContanst.UpWeapon);
            _flashAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.UpWeaponFlash);
            _colliderAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.UpWeaponCollider);

        } 
        else if (temp == DirectionWeaponContanst.DownWeapon) 
        {
            _swordNormal.RotateWeapon(DirectionWeaponContanst.DownWeapon);
            _flashAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.DownWeaponFlash);
            _colliderAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.DownWeaponCollider);
        } 
        else if (temp == DirectionWeaponContanst.RightWeapon) 
        {
            _swordNormal.RotateWeapon(DirectionWeaponContanst.RightWeapon);
            _flashAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.RightWeaponFlash);
            _colliderAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.RightWeaponCollider);
        } 
        else if (temp == DirectionWeaponContanst.LeftWeapon) 
        {
            _swordNormal.RotateWeapon(DirectionWeaponContanst.LeftWeapon);
            _flashAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.LeftWeaponFlash);
            _colliderAttackWeapon.transform.rotation = Quaternion.Euler(_infoWeaponTransform.LeftWeaponCollider);
        }     
    }

    public string CheckDirectionWeapon(Vector2 inputPlayer)
    {
        if (inputPlayer.y > validToChangeDirWeapon)
        {
            // trên
            return DirectionWeaponContanst.UpWeapon;
        }
        else if (inputPlayer.y < -validToChangeDirWeapon)
        {
            // dưới
            return DirectionWeaponContanst.DownWeapon;
        }
        else if (inputPlayer.x > validToChangeDirWeapon)
        {
            // phải
            return DirectionWeaponContanst.RightWeapon;
        }
        else if (inputPlayer.x < -validToChangeDirWeapon)
        {
            // trái
            return DirectionWeaponContanst.LeftWeapon;
        }
        else
        {
            Debug.Log("LogError - Return Null - CheckDirectionWeapon");
            return null;
        }
    }

    public void TakeDamageEnemy(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemeHeal>())
        {
            EnemeHeal enemeHeal = other.gameObject.GetComponent<EnemeHeal>();
            enemeHeal.TakeDamage(_damageCalculator.CalculateDamage(_WeaponPlayerData));
        }
    }

    // Animation event
    public void BeginAttack()
    {
        _colliderAttackWeapon.gameObject.SetActive(true);
        _flashAttackWeapon.SetActive(true);
    }

    // Animation event
    public void EndAttack()
    {
        _colliderAttackWeapon.gameObject.SetActive(false);
    }
}

public class DirectionWeaponContanst
{
    public const string UpWeapon = "Up";
    public const string DownWeapon = "Down";
    public const string LeftWeapon = "Left";
    public const string RightWeapon = "Right";
}

public class DamageCalculator
{
    public int CalculateDamage(WeaponPlayerData weaponData)
    {
        int baseDamage = weaponData.baseDamage;
        int calculatedDamage = baseDamage; // Bạn có thể thêm các yếu tố tính toán khác tại đây

        return calculatedDamage;
    }
}

public class WeaponData
{
    public string weaponName = "sword";
    public bool isCriticalHit = true;
    public int baseDamage = 1;
    public float attackCooldown = 1f;
}