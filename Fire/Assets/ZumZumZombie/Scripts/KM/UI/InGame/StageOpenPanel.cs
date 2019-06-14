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

    private float scaleInTime = 1f;
    private float colorTime = 1f;
    private float scaleOutTime = 0.3f;
    private float scaleOutScale = 200.0f;
    private float scaleOutDisappearTime = 0.5f;

    private Color myColor;

    private void Start()
    {
        targetScale = targetText.GetComponent<RectTransform>().localScale;
        targetColor = targetText.GetComponent<Text>().color;
        ClosePanel();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        PlayTweenEffect();
    }

    public void ClosePanel()
    {
        resetText1();
        gameObject.SetActive(false);
    }

    private void resetText1()
    {
        targetforScaleOutText.SetActive(false);
        targetText.GetComponent<RectTransform>().localScale = Vector3.zero;
        targetText.GetComponent<Text>().color = Color.gray;
    }

    private void resetText2()
    {
        targetforScaleOutText.SetActive(true);
        targetforScaleOutText.GetComponent<RectTransform>().localScale = Vector3.one;
        targetforScaleOutText.GetComponent<Text>().color = Color.gray;
    }

    private void PlayTweenEffect()
    {
        TweenScaleIn();
    }

    private void TweenScaleIn_Complete()
    {
        resetText2();
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

    private void TweenScaleIn()
    {
        LTDescr desc = LeanTween.scale(targetText, targetScale, scaleInTime).setEase(LeanTweenType.easeOutElastic);
        desc.setOnComplete(TweenScaleIn_Complete);
    }

    private void TweenColor()
    {
        myColor = Color.gray;
        LTDescr desc = LeanTween.value(0f, Color.gray.r, colorTime);
        desc.setOnUpdate(ColorValueUpdate);
        desc.setOnUpdate(x => { ColorValueUpdate(x); });
        desc.setOnComplete(TweenColor_Complete);
    }

    private void ColorValueUpdate(float value)
    {
        myColor.g = (Color.gray.g - value);
        myColor.b = (Color.gray.b - value);
        myColor.r = (Color.gray.r + value);

        targetText.GetComponent<Text>().color = myColor;
    }

    private void TweenScaleOut()
    {
        LeanTween.alphaText(targetText.GetComponent<RectTransform>(), 0f, scaleOutTime * scaleOutDisappearTime).setEase(LeanTweenType.easeInCirc);
        LeanTween.alphaText(targetforScaleOutText.GetComponent<RectTransform>(), 0f, scaleOutTime * scaleOutDisappearTime).setEase(LeanTweenType.easeInCirc);

        LTDescr desc = LeanTween.scale(targetText, targetScale * scaleOutScale, scaleOutTime).setEase(LeanTweenType.easeInCirc);
        LTDescr desclate = LeanTween.scale(targetforScaleOutText, targetScale * scaleOutScale, scaleOutTime * 2f).setEase(LeanTweenType.easeInCirc);

        desc.setOnComplete(TweenScaleOut_Complete);
    }
}