using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageOpenPanel : MonoBehaviour
{
    public GameObject targetText;
    public GameObject targetforScaleOutText;

    private Vector3 targetScale;
    private Color targetColor;
    private Color currColor = Color.red;

    private float scaleInTime = 1.5f;
    private float colorTime = 0.3f;
    private float scaleOutTime = 0.5f;
    private float scaleOutScale = 200.0f;
    private float scaleOutDisappearTime = 0.5f;

    private string stageOpenPanelText;

    private void Awake()
    {
        targetScale = targetText.GetComponent<RectTransform>().localScale;
        targetText.GetComponent<Text>().color = currColor;
    }

    private void Start()
    {
        ClosePanel();
    }

    public void OpenPanel(string stageName)
    {
        if (LeanTween.isTweening(targetforScaleOutText))
        {
            Debug.Log("--");
            return;
        }
        gameObject.SetActive(true);

        stageOpenPanelText = string.Format("*STAGE OPEN*\n{0}", stageName);
        resetPanel(stageOpenPanelText);
        PlayTweenEffect();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void PlayTweenEffect()
    {
        TweenScaleIn();
    }

    private void TweenScaleIn_Complete()
    {
        TweenColor();
    }

    private void TweenColor_Complete()
    {
        TweenScaleOut();
    }

    private void TweenScaleOut_Complete()
    {
        ClosePanel();
    }

    private void resetPanel(string stageOpenPanelText)
    {
        targetforScaleOutText.GetComponent<Text>().text = stageOpenPanelText;
        targetforScaleOutText.GetComponent<RectTransform>().localScale = Vector3.one * 1.05f;
        targetforScaleOutText.GetComponent<Text>().color = Color.black;

        targetText.GetComponent<Text>().text = stageOpenPanelText;
        targetText.GetComponent<RectTransform>().localScale = Vector3.zero;
        targetText.GetComponent<Text>().color = currColor;
    }

    private void TweenScaleIn()
    {
        LTDescr desc = LeanTween.scale(targetText, targetScale, scaleInTime).setEase(LeanTweenType.easeOutElastic);
        desc.setOnComplete(TweenScaleIn_Complete);
    }

    private void TweenColor()
    {
        targetColor = currColor;
        Debug.Log(targetColor);

        LTDescr desc = LeanTween.value(0f, currColor.r, colorTime);
        desc.setOnUpdate(ColorValueUpdate);
        desc.setOnUpdate(x => { ColorValueUpdate(x); });
        desc.setOnComplete(TweenColor_Complete);
    }

    private void ColorValueUpdate(float value)
    {
        targetColor.g = (currColor.g - value);
        targetColor.b = (currColor.b - value);
        targetColor.r = (currColor.r + value);

        targetText.GetComponent<Text>().color = targetColor;
    }

    private void TweenScaleOut()
    {
        LeanTween.alphaText(targetText.GetComponent<RectTransform>(), 0f, scaleOutTime * scaleOutDisappearTime).setEase(LeanTweenType.easeInCirc);
        LeanTween.alphaText(targetforScaleOutText.GetComponent<RectTransform>(), 0f, scaleOutTime * scaleOutDisappearTime).setEase(LeanTweenType.easeInCirc);

        LTDescr desc = LeanTween.scale(targetText, targetScale * scaleOutScale, scaleOutTime).setEase(LeanTweenType.easeInCirc);
        LTDescr desclate = LeanTween.scale(targetforScaleOutText, targetScale * 10f, scaleOutTime).setEase(LeanTweenType.easeInCirc);

        desc.setOnComplete(TweenScaleOut_Complete);
    }
}