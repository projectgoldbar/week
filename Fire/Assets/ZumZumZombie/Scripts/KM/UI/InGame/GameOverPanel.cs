using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public GameObject adsButton;
    public GameObject doubleCoinText;
    public GameObject normalCoinText;

    private void start()
    {
        //Open_gameOverPanel();
    }

    public void Open_gameOverPanel()
    {
        resetBeforeAds();
        gameObject.SetActive(false);
    }

    public void OnAds_Button()
    {
        ChangeAdsSettings();
    }

    private void resetBeforeAds()
    {
        adsButton.SetActive(true);
        normalCoinText.SetActive(true);
        doubleCoinText.SetActive(false);
    }

    public void ChangeAdsSettings()
    {
        adsButton.SetActive(false);
        normalCoinText.SetActive(false);
        doubleCoinText.SetActive(true);
    }
}