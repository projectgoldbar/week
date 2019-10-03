using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkinBoxOpenPanel : MonoBehaviour
{
    public GameObject skinBoxsLayer;

    public GameObject skinBoxObject;
    public GameObject skinBoxOpenObject;

    public GameObject skinBoxPanelTextObj;

    public GameObject openBoxButton;
    public GameObject getBoxButton;

    private float IntroTime = 0.5f;

    private float rotTime = 2.0f;
    private float scaleTime = 1.5f;

    private Quaternion skinBoxRotQ;
    private Vector3 targetSkinBoxScaleforIntro;

    private string skinBoxPanelText;

    private Vector2 openPostion = new Vector2(0, 0);
    private Vector2 closePostion = new Vector2(1500, 0);

    private void Awake()
    {
        skinBoxsLayer.SetActive(true);

        targetSkinBoxScaleforIntro = skinBoxObject.GetComponent<Transform>().localScale;
        skinBoxRotQ = skinBoxObject.GetComponent<Transform>().rotation;
    }

    private void Start()
    {
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePostion;
        skinBoxObject.SetActive(false);
        skinBoxOpenObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().anchoredPosition = openPostion;

        skinBoxObject.SetActive(true);
        skinBoxOpenObject.SetActive(false);

        resetPanel();
        PlayTweenEffectforOpen();
    }

    private void PlayTweenEffectforOpen()
    {
        IntroBox();
    }

    private void IntroBox_Complete()
    {
        openBoxButton.SetActive(true);
    }

    private void resetPanel()
    {
        skinBoxObject.GetComponent<Transform>().localScale = Vector3.zero;
        skinBoxObject.GetComponent<Transform>().rotation = skinBoxRotQ;
        openBoxButton.SetActive(false);
        getBoxButton.SetActive(false);
    }

    private void IntroBox()
    {
        LTDescr decr = LeanTween.scale(skinBoxObject, targetSkinBoxScaleforIntro, IntroTime).setEase(LeanTweenType.easeOutElastic);
        decr.setOnComplete(IntroBox_Complete);
    }
}