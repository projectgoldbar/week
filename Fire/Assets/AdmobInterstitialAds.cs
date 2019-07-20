using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobInterstitialAds : MonoBehaviour
{
    private InterstitialAd screenAd;
    private string UnitId = "ca-app-pub-9641742132924690/4254827283";

    void Start()
    {
        RequstAd();
    }

    void RequstAd()
    {
        screenAd = new InterstitialAd(UnitId);

        AdRequest request = new AdRequest.Builder()
       .TagForChildDirectedTreatment(true)
       .AddExtra("max_ad_content_rating", "G")
       .Build();

        screenAd.LoadAd(request);
        screenAd.OnAdClosed += AdClose;
    }

    public void AdClose(object sender , EventArgs e)
    {
        screenAd.Destroy();
        RequstAd();
    }

    public void AdShow()
    {
        if(screenAd.IsLoaded())
        screenAd.Show();
    }
}
