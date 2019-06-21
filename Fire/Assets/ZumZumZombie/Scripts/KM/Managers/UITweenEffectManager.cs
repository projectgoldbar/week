using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITweenEffectManager : MonoBehaviour
{
    private static UITweenEffectManager instance;

    public static UITweenEffectManager Instace
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public StageOpenPanel stageOpenPanel;
    public SkinBoxOpenPanel skinBoxOpenPanel;
    public GameOverPanel gameOverPanel;
    public GameObject FadeInImageObj;

    private float duration = 3.0f;
    private float from = 255.0f;
    private float to = 0.0f;
    private Color beforeFAdeInColor;
    private Color nowColor;

    private void Start()
    {
        resetPanel();
        startGame();
    }

    private void resetPanel()
    {
        beforeFAdeInColor.a = from;
        FadeInImageObj.GetComponent<Image>().color = beforeFAdeInColor;
    }

    public void startGame()
    {
        FadeInEffect();
    }

    private void FadeInEffect()
    {
        var d = LeanTween.value(from, to, duration);
        d.setOnUpdate(x => { ValueUpdateFadeOut(x); });
        d.setOnComplete(FadeInImageComplete);
    }

    private void ValueUpdateFadeOut(float value)
    {
        nowColor.a = value / 255f;
        FadeInImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeInImageComplete()
    {
        Debug.Log("FadeInEnd");
        //게임시작 부르기!
    }
}