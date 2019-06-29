using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITweenEffectManager : MonoBehaviour
{
    public StageOpenPanel stageOpenPanel;
    public SkinBoxOpenPanel skinBoxOpenPanel;
    public GameOverPanel gameOverPanel;
    public PausePanel pausePanel;

    public GameObject FadeInImageObj;

    public bool isCompleteFadeIn = false;
    public bool isCompleteFadeOut = false;

    public float fadeInDuration = 2.0f;

    private float toOut = 255.0f;
    private float toIn = 0.0f;
    private Color beforeFAdeInColor;
    private Color nowColor;

    private void resetPanel(float resetAlpha)
    {
        beforeFAdeInColor.a = resetAlpha;
        FadeInImageObj.GetComponent<Image>().color = beforeFAdeInColor;
    }

    public void EnterInGame()
    {
        resetPanel(toOut);
        FadeInEffect();
    }

    public void LeaveInGame()
    {
        resetPanel(toIn);
        FadeOutEffect();
    }

    private void FadeInEffect()
    {
        FadeInImageObj.GetComponent<Image>().raycastTarget = false;

        isCompleteFadeIn = false;
        var d = LeanTween.value(toOut, toIn, fadeInDuration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeInImageComplete);
    }

    private void FadeOutEffect()
    {
        FadeInImageObj.GetComponent<Image>().raycastTarget = true;

        isCompleteFadeOut = false;
        var d = LeanTween.value(toIn, toOut, fadeInDuration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeOutImageComplete);
    }

    private void ValueUpdateFade(float value)
    {
        nowColor.a = value / 255f;
        FadeInImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeInImageComplete()
    {
        isCompleteFadeIn = true;
    }

    private void FadeOutImageComplete()
    {
        isCompleteFadeOut = true;
    }

    public void PauseGameOpen()
    {
        //매니저가 게임 일시정지한 후
        pausePanel.OpenPanel();//패널띄우기!
    }

    public void PauseGameClose()
    {
        pausePanel.ClosePanel();
    }

    public void GiveUpGame()
    {
        pausePanel.GiveUp();//게임 저장하고 / 게임끄기!
    }
}