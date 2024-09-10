using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventUIForPlayer : Singleton<EventUIForPlayer>
{
    public TMP_Text GoldText;
    public Button inventoryBtn;
    public Slider healthSliderPlayer;
    public Button InterstitialBtn;
    public Button RewardedBtn;

    protected override void Awake()
    {
        base.Awake();
       
    }

    private void Start()
    {
        inventoryBtn.onClick.AddListener(ShowInventory);
        InterstitialBtn.onClick.AddListener(ShowInterstitialAds);
        RewardedBtn.onClick.AddListener(ShowRewardedAds);
    }

    private void ShowInterstitialAds()
    {
       AdsManager.Instance.ShowInterstitialAd();
    }

    private void ShowRewardedAds()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

    private void ShowInventory()
    {
        PopupManager.Instance.SetPopupInventory(true);
    }
}
