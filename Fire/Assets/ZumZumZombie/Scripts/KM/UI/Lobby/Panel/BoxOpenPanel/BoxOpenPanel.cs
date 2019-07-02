using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BoxOpenPanel : MonoBehaviour
{
    public Vector2 closePostion;

    public GameObject boxObj;
    public GameObject boxOpenObj;
    public GameObject resultPanel;
    public GameObject resultCardBackObj;
    public GameObject resultCardFrontObj;

    public GameObject unBoxingButton;
    public GameObject closePanelButton;

    public ParticleSystem absorbVfx;
    public ParticleSystem explodeVfx;
    public ParticleSystem openVfx;

    private float IntroTime = 0.5f;

    private float rotTime = 2.0f;
    private float scaleTime = 1.5f;
    private float showResultTweenTime = 0.5f;

    private Quaternion boxRotQ;
    private Vector3 targetBoxScaleforIntro;
    private Vector3 openBoxScaleforIntro;

    private Vector2 openPosition = new Vector2(0, 0);

    private Vector2 closeResultPostion = new Vector2(2000, 2500);
    private Vector2 closeCardBackPosition = new Vector2(0, -1000);
    private Vector2 closeCardFrontPosition = new Vector2(0, -1500);

    private void Awake()
    {
        targetBoxScaleforIntro = boxObj.GetComponent<Transform>().localScale;
        openBoxScaleforIntro = boxOpenObj.GetComponent<Transform>().localScale;
        boxRotQ = boxObj.GetComponent<Transform>().rotation;
    }

    private void Start()
    {
        Close();
        StopAllVfx();
    }

    private void StopAllVfx()
    {
        absorbVfx.Stop();
        explodeVfx.Stop();
    }

    public void Close()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePostion;
        resultPanel.GetComponent<RectTransform>().anchoredPosition = closeResultPostion;
        resultCardBackObj.GetComponent<RectTransform>().anchoredPosition = closeCardBackPosition;
        resultCardFrontObj.GetComponent<RectTransform>().anchoredPosition = closeCardFrontPosition;
    }

    public void Open()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = openPosition;

        boxObj.SetActive(true);
        boxOpenObj.SetActive(false);

        resetPanel();
        PlayTweenEffectforOpen();
    }

    private void PlayTweenEffectforOpen()
    {
        IntroBox();
    }

    private void IntroBox_Complete()
    {
        unBoxingButton.SetActive(true);
    }

    private void resetPanel()
    {
        boxObj.GetComponent<Transform>().localScale = Vector3.zero;
        boxObj.GetComponent<Transform>().rotation = boxRotQ;
        unBoxingButton.SetActive(false);
        closePanelButton.SetActive(false);
        resultPanel.GetComponent<RectTransform>().anchoredPosition = closeResultPostion;
        resultCardBackObj.GetComponent<RectTransform>().anchoredPosition = closeCardBackPosition;
        resultCardBackObj.GetComponent<RectTransform>().localScale = Vector3.zero;
        resultCardFrontObj.GetComponent<RectTransform>().anchoredPosition = closeCardFrontPosition;
    }

    private void IntroBox()
    {
        LTDescr decr = LeanTween.scale(boxObj, targetBoxScaleforIntro, IntroTime).setEase(LeanTweenType.easeOutElastic);
        decr.setOnComplete(IntroBox_Complete);
    }
}