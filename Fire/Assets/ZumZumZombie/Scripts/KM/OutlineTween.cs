using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTween : MonoBehaviour
{
    public float targetScale = 1.3f;
    public float durationScale = 1.8f;

    private Color nowColor;
    private float onAlpha;
    private float offAlpha = 0;

    private Vector3 myPos;
    private Vector3 myScale;
    private Color myColor;

    private SpriteRenderer spriteDoorOutLine;

    private void Start()
    {
        spriteDoorOutLine = gameObject.GetComponent<SpriteRenderer>();

        myPos = gameObject.GetComponent<Transform>().position;
        myScale = gameObject.GetComponent<Transform>().localScale;
        myColor = gameObject.GetComponent<SpriteRenderer>().color;

        onAlpha = myColor.a * 255f;
        nowColor = myColor;

        DoorOutLineIdle();
    }

    private void DoorOutLineIdle()
    {
        LeanTween.scale(gameObject, Vector3.one * myScale.x * targetScale, durationScale).setEase(LeanTweenType.easeOutQuart).setLoopClamp();
        LTDescr d = LeanTween.value(gameObject, onAlpha, offAlpha, durationScale).setLoopClamp();
        d.setOnUpdate(x => { ValueUpdateAlpha(x); });
    }

    private void ValueUpdateAlpha(float value)
    {
        nowColor.a = value / 255f;
        gameObject.GetComponent<SpriteRenderer>().color = nowColor;
    }
}