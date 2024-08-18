using System;
using UnityEngine;
using GoogleMobileAds.Api;

// 보상형 광고를 요청하는 클래스입니다.
public class RewardAdRequest : MonoBehaviour
{
    private static RewardAdRequest instance;
    private RewardedInterstitialAd rewardedInterstitialAd;

    string adUnitId;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        	//초기화 완료
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
            adUnitId = "unexpected_platform";
#endif

        LoadRewardedInterstitialAd();
    }

    // 보상형 광고를 요청합니다
    public void LoadRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
                rewardedInterstitialAd.Destroy();
                rewardedInterstitialAd = null;
        }
        
        var adRequest = new AdRequest();
        RewardedInterstitialAd.Load(adUnitId, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                rewardedInterstitialAd = ad;
            });
    }

    // 보상형 광고를 시청합니다
    public static void ShowAd(Action beforeAction, Action rewardAction, Action afterAction)
    {
        if (CanShowAd())
        {
            Action onAdClosed = null;
            onAdClosed = () =>
            {
                afterAction?.Invoke();
                instance.rewardedInterstitialAd.OnAdFullScreenContentClosed -= onAdClosed;
            };
            instance.rewardedInterstitialAd.OnAdFullScreenContentClosed += onAdClosed;

            beforeAction?.Invoke();
            instance.rewardedInterstitialAd.Show((Reward reward) =>
            {
                rewardAction?.Invoke();
            });
        }
    }

    // 보상형 광고를 시청할 수 있는지 확인합니다
    public static bool CanShowAd()
    {
        if(instance == null)
        {
            return false;
        }

        if(instance.rewardedInterstitialAd == null)
        {
            return false;
        }

        if(!instance.rewardedInterstitialAd.CanShowAd())
        {
            instance.LoadRewardedInterstitialAd();
            return instance.rewardedInterstitialAd.CanShowAd();
        }

        return true;
    }
}