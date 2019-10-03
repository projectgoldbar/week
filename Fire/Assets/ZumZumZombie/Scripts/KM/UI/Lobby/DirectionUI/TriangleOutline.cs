using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriangleOutline : MonoBehaviour
{
    public float targetScale = 1.1f;
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
        LeanTween.scale(gameObject, Vector3.one * targetScale, durationT).setDelay(3.0f).setEase(LeanTweenType.easeOutQuart).setLoopClamp();
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, durationT).setDelay(3.0f).setLoopClamp();
        d.setOnUpdate(x => { ValueUpdateAlpha(x); });
    }

    private void ValueUpdateAlpha(float value)
    {
        nowColor.a = value / 255f;
        gameObject.GetComponent<Image>().color = nowColor;
    }
}