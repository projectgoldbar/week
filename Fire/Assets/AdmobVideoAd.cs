using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdmobVideoAd : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-5205187543072249/3297754282";
    private readonly string test_Ad_unitID = "ca-app-pub-3940256099942544/5224354917";    // 동영상
    private readonly string test_deviceId = "099176D0E511411B";

    private RewardBasedVideoAd RewardAd;

    private readonly string AppId = "ca-app-pub-5205187543072249~8311581277";
    private readonly string TestAppId = "ca-app-pub-3940256099942544~3347511713";

    public UnityEvent AdsReward;
    public UnityEvent UnityAdsReward;
    public UnityEvent AdsFail;

    public bool AdsPlaying = false;

    // Start is called before the first frame update
    private void Start()
    {
        //MobileAds.Initialize(AppId);
        MobileAds.Initialize(TestAppId);
        RewardAd = RewardBasedVideoAd.Instance;
        //광고 요청이 성공적으로 로드되면 호출됩니다.
        RewardAd.OnAdLoaded += (sender, e) =>
        {
            Debug.Log("OnAdLoaded");
        };
        //광고요청을 로드하지 못했을때 호출됩니다.
        RewardAd.OnAdFailedToLoad += (sender, e) =>
        {
            Debug.Log("OnAdFailedToLoad");
        };

        //광고가 표시활때 호출됩니다.
        RewardAd.OnAdOpening += (sender, e) => Debug.Log("OnAdOpening");
        //광고가 재생되기 시작하면 호출됩니다.
        RewardAd.OnAdStarted += (sender, e) => Debug.Log("OnAdStarted");
        //사용자가 비디오시청을 통해 보상을 받을 때 호출됩니다.
        RewardAd.OnAdRewarded += (sender, e) => { Debug.Log("OnAdRewarded"); AdsReward?.Invoke(); };
        //광고가 닫힐때 호출됩니다.
        RewardAd.OnAdClosed += (sender, e) =>
        {
            Debug.Log("OnAdClosed");
            AdsPlaying = false;
            LoadAd();
            //UITweenEffectManager.Instance.gameOverPanel.OnAds_Button();
        };
        //광고클릭으로 인해 사용자가 애플리케이션을 종료한 경우 호출됩니다.
        RewardAd.OnAdLeavingApplication += (sender, e) => Debug.Log("OnAdLeavingApplication");

        LoadAd();
    }

    private void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        RewardAd.LoadAd(request, test_Ad_unitID);
    }

    public void ShowRewardedAd()
    {
        if (this.RewardAd.IsLoaded())
        {
            AdsPlaying = true;
            this.RewardAd.Show();
            //UITweenEffectManager.Instance.gameOverPanel.OnAds_Button();
        }
        else
        {
            Debug.Log("NOT Loaded Interstitial");
            LoadAd();
            //유니티애즈 실행
            UnityAdsReward?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        if (AdsPlaying)
            AdsFail?.Invoke();
    }
}