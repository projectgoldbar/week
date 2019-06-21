using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    public float duration = 3.0f;
    private float from = 0.0f;
    private float to = 255.0f;

    public GameObject fadeOutImageObj;
    private Color beforeFAdeOutColor;
    private Color nowColor;

    private void resetFadeOut()
    {
        beforeFAdeOutColor = fadeOutImageObj.GetComponent<Image>().color;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
    }

    public void FlowBeforePlay()
    {
        FadeOutImage();
    }

    private void FadeOutImage()
    {
        var d = LeanTween.value(from, to, duration).setDelay(duration * 0.3f);
        d.setOnUpdate(x => { ValueUpdateFadeOut(x); });
        d.setOnComplete(FadeOutImageComplete);
    }

    private void ValueUpdateFadeOut(float value)
    {
        nowColor.a = value / 255f;
        fadeOutImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeOutImageComplete()
    {
        Debug.Log("FadeOutEnd");
        //게임종료 부르기!
    }
}