using System;
using UnityEngine;
using GoogleMobileAds.Api;


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

  public void LoadRewardedInterstitialAd()
  {
      if (rewardedInterstitialAd != null)
      {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
      }

      Debug.Log("Loading the rewarded interstitial ad.");
      

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

              Debug.Log("Rewarded interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              rewardedInterstitialAd = ad;
          });
  }

    public static void ShowAd(Action beforeAction, Action rewardAction, Action afterAction) //광고 보기
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
    

    private void RegisterReloadHandler(RewardedAd ad) //광고 재로드
    {
        ad.OnAdFullScreenContentClosed += (null); 
        {
            Debug.Log("Rewarded Ad full screen content closed.");
            LoadRewardedInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardedInterstitialAd();
        };
    }
}