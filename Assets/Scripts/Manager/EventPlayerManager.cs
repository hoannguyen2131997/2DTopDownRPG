using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsPlayerManager: MonoBehaviour
{
    public event EventHandler<OnTakeDamePressedEventArgs> OnPlayerTakeDame;
    public event EventHandler<OnAttackPressedEventArgs> OnPlayerAttack;

    public void EndAttackingPlayer(InputAction.CallbackContext context)
    {
        OnPlayerAttack?.Invoke(this, new OnAttackPressedEventArgs { PressAttacking = false });
    }

    public void StartAttackingPlayer(InputAction.CallbackContext context)
    {
        OnPlayerAttack?.Invoke(this, new OnAttackPressedEventArgs { PressAttacking = true });
    }

    public void GetDamegePlayer(int damege)
    {
        OnPlayerTakeDame?.Invoke(this, new OnTakeDamePressedEventArgs { DamageTaken = damege });
    }
}

public class OnTakeDamePressedEventArgs : EventArgs
{
    public int DamageTaken;
}

public class OnAttackPressedEventArgs : EventArgs
{
    public bool PressAttacking;
}
