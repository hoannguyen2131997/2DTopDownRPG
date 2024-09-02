using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    private EventsPlayerManager eventsPlayerManager;
    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking;
    private bool isBlockWeapon;


    protected override void Awake()
    {
        base.Awake();
     
        eventsPlayerManager = GetComponentInParent<EventsPlayerManager>();


    }

    private void Start()
    {
        //GameInputSystemSingleton.Instance.OnAttacking += Attack;
        if (eventsPlayerManager != null)
        {
            eventsPlayerManager.OnPlayerAttack += Attack;
            eventsPlayerManager.OnBlockControlPlayer += OnBlockWeapon;
        }
        else
        {
            Debug.Log("event player manager is null ");
        }
        AttackCooldown();
    }

    private void OnBlockWeapon(object sender, OnBlockControlPlayerEventArgs e)
    {
        isBlockWeapon = e.IsBlockControlPlayer;
    }

    private void Attack(object sender, OnAttackPressedEventArgs e)
    {
        if(!isBlockWeapon)
        {
            attackButtonDown = e.PressAttacking;

            if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
            {
                AttackCooldown();
                (CurrentActiveWeapon as IWeapon).Attack();
            }
        }
    }

    public void ToggleIsAttacking(bool _isAttacking)
    {
        isAttacking = _isAttacking;
    }

    public void WeaponNull()
    {
       CurrentActiveWeapon = null;
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
}
