using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinBoxOpenPanel : MonoBehaviour
{
    public GameObject skinBoxObject;
    public GameObject skinBoxPanelTextObj;
    public GameObject getButton;

    private Vector3 skinBoxRot;

    private float IntroTime = 1.0f;

    private float rotTime = 3.0f;
    private float scaleTime = 1.5f;

    private Vector3 targetScaleforSkinBox;

    private string skinBoxPanelText;

    private void Awake()
    {
        targetScaleforSkinBox = skinBoxObject.GetComponent<Transform>().localScale;
    }

    private void Start()
    {
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        skinBoxObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        skinBoxObject.SetActive(true);

        resetPanel();
        PlayTweenEffectforOpen();
    }

    private void resetPanel()
    {
        skinBoxObject.GetComponent<Transform>().localScale = Vector3.zero;
        getButton.SetActive(false);
    }

    private void PlayTweenEffectforOpen()
    {
        IntroBox();
    }

    private void IntroBox_Complete()
    {
        getButton.SetActive(true);
    }

    private void PlayTweenEffectForButton_Complete()
    {
        Close();
    }

    private void IntroBox()
    {
        LTDescr decr = LeanTween.scale(skinBoxObject, targetScaleforSkinBox, IntroTime).setEase(LeanTweenType.easeOutElastic);
        decr.setOnComplete(IntroBox_Complete);
    }

    public void OnGetButton()
    {
        if (LeanTween.isTweening(skinBoxObject))
        {
            Debug.Log("박스도는중");
            return;
        }

        PlayTweenEffectForButton();
    }

    private void PlayTweenEffectForButton()
    {
        Vector3 to = Vector3.zero;
        to.y = skinBoxRot.y;

        skinBoxObject.transform.eulerAngles = Vector3.zero;
        transform.localScale = Vector3.zero;
        //LeanTween.scale(skinBoxObject, Vector3.one, scaleTime).setEase(LeanTweenType.easeOutElastic);

        LTDescr desc = LeanTween.rotate(skinBoxObject, Vector3.up * 1080f, rotTime).setEase(LeanTweenType.easeOutExpo);
        desc.setOnComplete(PlayTweenEffectForButton_Complete);
    }
}