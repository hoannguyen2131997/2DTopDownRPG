using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomManager : Singleton<EconomManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private TMP_Text goldText;
    private int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "GoldText";

    public void UpdateCurrentGold()
    {
        currentGold += 1;

        if(goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}
