using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-5615197344741041/6429978800";

    private readonly string test_unitID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string test_deviceId = "099176D0E511411B";

    private BannerView banner;

    public AdPosition position;

    private void Start()
    {
        InitAd();
    }


    private void InitAd()
    {
        string id = Debug.isDebugBuild ? test_unitID : unitID;

        banner = new BannerView(unitID, AdSize.SmartBanner,position);
        //빌드패턴
        //AdRequest request = new AdRequest.Builder().AddTestDevice(test_deviceId).Build();
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);

    }

    public void ToogleAd(bool action)
    {
        if (action) banner.Show();
        else banner.Hide();
    }

    public void DestroyAd()
    {
        banner.Destroy();
    }






}
