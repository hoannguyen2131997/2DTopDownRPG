//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AttackStateManager
//{
//    private IAttackState currentState;

//    public void SetInitialState(IAttackState initialState)
//    {
//        currentState = initialState;
//    }

//    public void SetState(IAttackState newState)
//    {
//        currentState = newState;
//    }

//    public void HandleAttack(WeaponManager weaponManager)
//    {
//        currentState.HandleAttack(this, weaponManager);
//    }
//}

//public class NormalAttackState : IAttackState
//{
//    public void HandleAttack(AttackStateManager attackStateManager, WeaponManager weaponManager)
//    {
//        Debug.Log("Performing normal attack!");
//        weaponManager.equippedWeapon.baseDamage += 5; // Ví dụ: thêm sát thương
//        weaponManager.ComboManager.IncrementCombo(); // Tăng combo

//        if (weaponManager.ComboManager.ComboCount >= 3)
//        {
//            attackStateManager.SetState(new SpecialAttackState()); // Chuyển sang trạng thái đòn đặc biệt
//        }
//    }
//}

//public class SpecialAttackState : IAttackState
//{
//    public void HandleAttack(AttackStateManager attackStateManager, WeaponManager weaponManager)
//    {
//        Debug.Log("Performing special attack with boosted damage!");
//        weaponManager.equippedWeapon.baseDamage *= 2; // Đòn đặc biệt có sát thương gấp đôi
//        weaponManager.ComboManager.ResetCombo(); // Reset combo
//        attackStateManager.SetState(new NormalAttackState()); // Quay lại trạng thái thường
//    }
//}

//public class ComboManager
//{
//    public int ComboCount { get; private set; }

//    public void IncrementCombo()
//    {
//        ComboCount++;
//        Debug.Log("Combo increased to: " + ComboCount);
//    }

//    public void ResetCombo()
//    {
//        ComboCount = 0;
//        Debug.Log("Combo reset!");
//    }
//}

//public interface IAttackState
//{
//    void HandleAttack(ComboManager comboManager);
//}
