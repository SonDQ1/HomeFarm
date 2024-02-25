using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Model;

public class AdApplovinController : Singleton<AdApplovinController>
{
    public bool IsBannerLoaded => true;
    public bool IsInterLoaded => true;
    public bool IsRewardLoaded => true;
    public Action OnInterClosed { get; set; }
    public Action OnInterLoaded { get; set; }
    public Action OnRewardClosed { get; set; }
    public Action OnRewardLoaded { get; set; }
    public Action OnRewardEarned { get; set; }
    [SerializeField] private ApplovinManager ad;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Init();
    }

    public void Init()
    {
        ad.InitAds();
        // this.OnInterClosed = OnInterClosed;
        // this.OnInterLoaded = OnInterLoaded;
        // this.OnRewardLoaded = OnRewardLoaded;
        // this.OnRewardClosed = OnRewardClosed;
        // this.OnRewardEarned = OnRewardEarned;
    }
    public void RequestBanner()
    {

    }
    public void ShowBanner()
    {
        ad.ShowBanner();
    }
    public void HideBanner()
    {
        ad.HideBanner();
    }
    public void RequestInterstitial()
    {

    }
    public void ShowInterstitial(Action acInter)
    {
        ad.ShowIntertitial(acInter);
    }
    public void RequestRewarded()
    {

    }
    public void ShowRewardedAd(Action onClaimReward)
    {
        ad.ShowReward((isShow) =>
        {
            if (isShow)
            {
                onClaimReward.Invoke();
            }
            OnRewardClosed?.Invoke();
        });
    }



}
