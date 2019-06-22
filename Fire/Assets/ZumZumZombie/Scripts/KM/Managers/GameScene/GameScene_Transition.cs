using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    public float duration = 4.0f;
    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    private Color beforeFAdeOutColor;
    private Color nowColor;

    public bool isCompleteFadeTween = false;

    private void resetFadeOut()
    {
        beforeFAdeOutColor = fadeOutImageObj.GetComponent<Image>().color;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
        FadeEffectTween(toOut, toIn, duration);
    }

    public void FlowBeforePlay()
    {
        FadeEffectTween(toIn, toOut, duration);
        Debug.Log(isCompleteFadeTween + "  _FlowBeforePlay");
    }

    public void FadeEffectTween(float from, float to, float duration)
    {
        Debug.Log("FadeTweenStart");
        isCompleteFadeTween = false;
        Debug.Log(isCompleteFadeTween + "  _FadeEffectTween");

        var d = LeanTween.value(from, to, duration);
        d.setOnUpdate(x => { ValueUpdateFadeOut(x); });
        d.setOnComplete(FadeEffectTweenComplete);
    }

    private void ValueUpdateFadeOut(float value)
    {
        nowColor.a = value / 255f;
        fadeOutImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeEffectTweenComplete()
    {
        Debug.Log("FadeTweenEnd");
        isCompleteFadeTween = true;
        Debug.Log(isCompleteFadeTween + "  _FadeEffectTweenComplete");
    }
}