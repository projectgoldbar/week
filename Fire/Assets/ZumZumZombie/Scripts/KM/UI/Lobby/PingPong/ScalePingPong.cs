using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalePingPong : MonoBehaviour
{
    private float durationT = 2f;
    private float delayT = 10f;

    private Color nowColor;
    private float onAlpha;
    private float offAlpha = 0;

    private void Start()
    {
        nowColor = gameObject.GetComponent<Image>().color;
        onAlpha = nowColor.a * 255f;

        Debug.Log(onAlpha + " : " + gameObject.GetComponent<Image>().color.a);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, durationT).setEase(LeanTweenType.easeOutQuart).setLoopClamp();
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, durationT).setLoopClamp();
        d.setOnUpdate(x => { valueUpdateAlpha(x); });
    }

    private void valueUpdateAlpha(float value)
    {
        nowColor.a = value / 255f;
        gameObject.GetComponent<Image>().color = nowColor;
    }
}