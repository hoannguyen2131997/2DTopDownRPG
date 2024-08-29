using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayerToSave : MonoBehaviour
{
    public string IDPlayer { get; private set; }
    public int maxHeal { get; private set; }
    public int currentHeal { get; private set; }

    public void UpdateMaxHeal(int _maxHeal)
    {
        maxHeal = _maxHeal;
    }

    public void UpdateCurrentHeal(int _currentHeal)
    {
        currentHeal = _currentHeal;
    }
}
