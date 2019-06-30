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

        RotBoxTweenEffect();
    }

    private void RotBoxTweenEffectForButton_Complete()
    {
        UnBoxingProcess();
    }

    private void UnBoxingProcess()
    {
        BoomEffect();
        ChangeMesh();
        OpenBoxTween();
        ShowResultTween();
    }

    private void ShowResultTween_Complete()
    {
        resultCardFrontObj.GetComponent<RectTransform>().anchoredPosition = openPosition;
        resultPanel.GetComponent<RectTransform>().anchoredPosition = openPosition;
        closePanelButton.SetActive(true);
    }

    private void BoomEffect()
    {
        //박스열릴때 나오는 연기 이팩트 같은거! 소리도 그렇고
    }

    private void ChangeMesh()
    {
        boxObj.SetActive(false);
        boxOpenObj.SetActive(true);

        unBoxingButton.SetActive(false);
    }

    private void RotBoxTweenEffect()
    {
        var d = LeanTween.rotate(boxObj, Vector3.up * 1080f * 3, rotTime).setEase(LeanTweenType.easeOutExpo);

        d.setOnComplete(RotBoxTweenEffectForButton_Complete);
    }

    private void OpenBoxTween()
    {
        if (LeanTween.isTweening(boxOpenObj))
        {
            return;
        }
        LTDescr decr = LeanTween.scale(boxOpenObj, openBoxScaleforIntro * 0.9f, IntroTime).setEase(LeanTweenType.easeOutElastic)
            .setLoopPingPong(3); //3번하고 끄긴하는데, 다중에 켯을때 ,남았으면, 그것회수까지만해서, 안움직일 경우도 있지만, 다시 열면 되긴해!
    }

    private void ShowResultTween()
    {
        resultCardBackObj.GetComponent<RectTransform>().anchoredPosition = openPosition;
        LTDescr d = LeanTween.scale(resultCardBackObj, Vector3.one, showResultTweenTime).setEase(LeanTweenType.easeInOutBack);
        d.setOnComplete(ShowResultTween_Complete);
    }

    private void ShowResult()
    {
        resultPanel.GetComponent<RectTransform>().anchoredPosition = openPosition;
    }
}