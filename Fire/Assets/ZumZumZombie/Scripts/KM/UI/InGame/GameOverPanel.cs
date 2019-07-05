using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject adsButton;
    public GameObject doubleCoinText;
    public GameObject normalCoinText;

    private Vector2 closePos = new Vector2(2000f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    private void Start()
    {
        Open_gameOverPanel();
    }

    private void OnEnable()
    {
        Open_gameOverPanel();
    }

    public void Open_gameOverPanel()
    {
        resetBeforeAds();
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
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