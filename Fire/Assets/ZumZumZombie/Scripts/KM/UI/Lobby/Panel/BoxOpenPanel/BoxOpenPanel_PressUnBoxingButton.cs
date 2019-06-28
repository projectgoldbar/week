using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BoxOpenPanel : MonoBehaviour
{
    public void OnPressUnBoxingBox_Button()
    {
        if (LeanTween.isTweening(boxObj))
        {
            Debug.Log("박스도는중");
            return;
        }

        RotBoxTweenEffectForButton();
    }

    private void RotBoxTweenEffectForButton_Complete()
    {
        OpentSkinBoxProcess();
    }

    //-----------

    private void RotBoxTweenEffectForButton()
    {
        var d = LeanTween.rotate(boxObj, Vector3.up * 1080f * 3, rotTime).setEase(LeanTweenType.easeOutExpo);
        d.setOnComplete(RotBoxTweenEffectForButton_Complete);
    }

    private void OpentSkinBoxProcess()
    {
        BoomEffect();
        ChangeMesh();
        IntroOpenBoxTween();
    }

    private void BoomEffect()
    {
    }

    private void ChangeMesh()
    {
        boxObj.SetActive(false);
        boxOpenObj.SetActive(true);

        unBoxingButton.SetActive(false);
        closePanelButton.SetActive(true);
    }

    private void IntroOpenBoxTween()
    {
        LTDescr decr = LeanTween.scale(boxOpenObj, targetBoxScaleforIntro * 0.9f, IntroTime).setEase(LeanTweenType.easeOutElastic)
            .setLoopPingPong();
    }
}