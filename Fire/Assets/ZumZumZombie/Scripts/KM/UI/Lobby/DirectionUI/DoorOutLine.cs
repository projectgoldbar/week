using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutLine : MonoBehaviour
{
    private float targetScale = 1.15f;
    private float durationT = 2f;
    private float delayT = 10f;

    private Color nowColor;
    private float onAlpha;
    private float offAlpha = 0;

    private void Start()
    {
        nowColor = gameObject.GetComponent<SpriteRenderer>().color;
        onAlpha = nowColor.a * 255f;

        LeanTween.scale(gameObject, Vector3.one * targetScale, durationT).setEase(LeanTweenType.easeOutQuart).setLoopClamp();
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, durationT).setLoopClamp();
        d.setOnUpdate(x => { ValueUpdateAlpha(x); });
    }

    private void ValueUpdateAlpha(float value)
    {
        nowColor.a = value / 255f;
        gameObject.GetComponent<SpriteRenderer>().color = nowColor;
    }
}