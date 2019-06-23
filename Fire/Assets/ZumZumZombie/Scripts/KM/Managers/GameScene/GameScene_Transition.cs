using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    public float duration = 4f;
    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    public bool isCompletedFadeOut = false;
    public bool isCompletedFadeIn = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBase_Controller lobbyBase_Controller;

    public void LeaveLobby()
    {
        if (LeanTween.isTweening())
        {
            Debug.Log(" LeaveLobby> " + LeanTween.tweensRunning + " // Don't ReLeaveLobby");
            return;
        }
        StartFadeImageAlpha(toIn);
        StartFadeOut();
        lobbyBase_Controller.lobbyPlayerController.RunningAnim();
    }

    public void EnterLobby()
    {
        if (LeanTween.isTweening())
        {
            Debug.Log("EnterLobby>  " + LeanTween.tweensRunning + "// Don't ReEnterLobby");
            return;
        }
        lobbyBase_Controller.lobbyPlayerController.ResetLobbyPlayer();
        lobbyBase_Controller.door.OpenDoor();
        StartFadeImageAlpha(toOut);
        StartFadeIn();
    }

    private void StartFadeImageAlpha(float startTo)
    {
        beforeFAdeOutColor.a = startTo;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
    }

    private void StartFadeOut()
    {
        isCompletedFadeOut = false;
        var d = LeanTween.value(toIn, toOut, duration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeOutTweenComplete);
    }

    private void StartFadeIn()
    {
        isCompletedFadeIn = false;
        var d = LeanTween.value(toOut, toIn, duration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeInTweenComplete);
    }

    private void ValueUpdateFade(float value)
    {
        nowColor.a = value / 255f;
        fadeOutImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeOutTweenComplete()
    {
        isCompletedFadeOut = true;
    }

    private void FadeInTweenComplete()
    {
        isCompletedFadeIn = true;
    }
}