using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-9641742132924690/3993468832";

    private readonly string test_unitID = "ca-app-pub-3940256099942544/6300978111";

    private BannerView banner;

    public AdPosition position;


    private void OnEnable()
    {
        AdbannerInit();
        Debug.Log($"admob이름 =  {gameObject.name}");
        
    }

    public void AdbannerInit()
    {
        banner = new BannerView(unitID, AdSize.MediumRectangle, position);
        //banner = new BannerView(test_unitID, AdSize.Banner, position);
        //빌드패턴
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);


        ToogleAd(UserDataManager.Instance.userData.AdOff);
    }

    public void ToogleAd(bool action)
    {
        if (!action) banner.Show();
        else banner.Hide();
    }


    private void OnDisable()
    {
       

        ToogleAd(true);
    }

}
