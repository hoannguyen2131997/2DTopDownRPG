using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsPlayerManager : MonoBehaviour
{
    public event EventHandler<OnTakeDamePressedEventArgs> OnPlayerTakeDame;
    public event EventHandler<OnAttackPressedEventArgs> OnPlayerAttack;
    public event EventHandler<OnBlockControlPlayerEventArgs> OnBlockControlPlayer;
    public event EventHandler<OnUpdateInventoryPlayerEventArgs> OnUpdateInventoryPlayer;
    public event EventHandler<OnShowInventoryPopupPlayerEventArgs> OnShowInventoryPopupPlayer;

    public void SetBlockControlPlayer(bool isBlockPlayer)
    {
        OnBlockControlPlayer?.Invoke(this, new OnBlockControlPlayerEventArgs { IsBlockControlPlayer = isBlockPlayer });
    }

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

    public void GetDataInventoryPlayer(DataItemInventory dataItemInventory)
    {
        OnUpdateInventoryPlayer?.Invoke(this, new OnUpdateInventoryPlayerEventArgs { DataItemInventory = dataItemInventory });
    }

    public void GetDataListItemInventory(List<DataItemInventory> _DataItemInventoryList)
    {
        OnShowInventoryPopupPlayer?.Invoke(this, new OnShowInventoryPopupPlayerEventArgs { DataItemInventoryList = _DataItemInventoryList });
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

public class OnBlockControlPlayerEventArgs : EventArgs
{
    public bool IsBlockControlPlayer;
}

public class OnUpdateInventoryPlayerEventArgs : EventArgs
{
    public DataItemInventory DataItemInventory;
}

public class OnShowInventoryPopupPlayerEventArgs : EventArgs
{
    public List<DataItemInventory> DataItemInventoryList;
}
