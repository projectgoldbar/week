using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutLine : MonoBehaviour
{
    public float leaveDuration = 4f;

    private float targetScale = 1.15f;
    private float durationT = 2f;
    private float delayT = 10f;

    private Color nowColor;
    private float onAlpha;
    private float offAlpha = 0;

    private Vector3 myPos;
    private Vector3 myScale;
    private Color myColor;

    private void Start()
    {
        myPos = gameObject.GetComponent<Transform>().position;
        myScale = gameObject.GetComponent<Transform>().localScale;
        myColor = gameObject.GetComponent<SpriteRenderer>().color;

        onAlpha = myColor.a * 255f;
        nowColor = myColor;

        DoorOutLineIdle();
    }

    private void DoorOutLineIdle()
    {
        LeanTween.scale(gameObject, Vector3.one * targetScale, durationT).setEase(LeanTweenType.easeOutQuart).setLoopClamp();
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, durationT).setLoopClamp();
        d.setOnUpdate(x => { ValueUpdateAlpha(x); });
    }

    private void ValueUpdateAlpha(float value)
    {
        nowColor.a = value / 255f;
        gameObject.GetComponent<SpriteRenderer>().color = nowColor;
    }

    public void ReactforLeaveLobby()
    {
        LeanTween.pause(gameObject);
        LeanTween.moveZ(gameObject, -4.5f, leaveDuration).setEase(LeanTweenType.easeOutExpo);
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, leaveDuration).setEase(LeanTweenType.easeOutExpo);
        d.setOnUpdate(x => { ValueUpdateAlpha(x); });
        d.setOnComplete(ResetDoorOutline);
    }

    public void ResetDoorOutline()
    {
        gameObject.GetComponent<Transform>().position = myPos;
        gameObject.GetComponent<Transform>().localScale = myScale;
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
        DoorOutLineIdle();
    }
}