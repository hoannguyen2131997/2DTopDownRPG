using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameInputSystemSingleton.Instance.OnAttacking += Attack;

        AttackCooldown();
    }

    private void Attack(object sender, GameInputSystemSingleton.OnAttackingPressedEventArgs e)
    {
        attackButtonDown = e.PressAttacking;

        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
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
