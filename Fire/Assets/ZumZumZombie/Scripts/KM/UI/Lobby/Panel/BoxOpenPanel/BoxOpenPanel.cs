using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BoxOpenPanel : MonoBehaviour
{
    public GameObject boxLayer;

    public GameObject boxObj;
    public GameObject boxOpenObj;

    public GameObject unBoxingButton;
    public GameObject closePanelButton;

    private float IntroTime = 0.5f;

    private float rotTime = 2.0f;
    private float scaleTime = 1.5f;

    private Quaternion boxRotQ;
    private Vector3 targetBoxScaleforIntro;

    private Vector2 openPostion = new Vector2(0, 0);
    private Vector2 closePostion = new Vector2(2500, 0);

    private void Awake()
    {
        boxLayer.SetActive(true);

        targetBoxScaleforIntro = boxObj.GetComponent<Transform>().localScale;
        boxRotQ = boxObj.GetComponent<Transform>().rotation;
    }

    private void Start()
    {
        Close();
    }

    public void Close()
    {
        //gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePostion;
        boxObj.SetActive(false);
        boxOpenObj.SetActive(false);
    }

    public void Open()
    {
        //gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().anchoredPosition = openPostion;

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
    }

    private void IntroBox()
    {
        LTDescr decr = LeanTween.scale(boxObj, targetBoxScaleforIntro, IntroTime).setEase(LeanTweenType.easeOutElastic);
        decr.setOnComplete(IntroBox_Complete);
    }
}