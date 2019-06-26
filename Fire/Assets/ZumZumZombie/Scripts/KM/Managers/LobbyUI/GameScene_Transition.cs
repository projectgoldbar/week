using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    private float LeaveDuration = 4f;
    private float EnterDuration = 3f;

    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    private bool isCompletedFadeOut = false;
    private bool isCompletedFadeIn = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBase_Controller lobbyBase_Controller;

    public void LeaveLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
            Debug.Log(" LeaveLobby> " + LeanTween.tweensRunning + " // Don't ReLeaveLobby");
            return;
        }
        LeaveLobbyProcess();
    }

    private void LeaveLobbyProcess()
    {
        StartFadeImageAlpha(toIn);
        lobbyBase_Controller.lobbyPlayerController.RunningAnim();
        StartFadeOut();
        UpPanel();
    }

    public void EnterLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
            Debug.Log("EnterLobby>  " + LeanTween.tweensRunning + "// Don't ReEnterLobby");
            return;
        }

        EnterLobbyProcess();
    }

    private void EnterLobbyProcess()
    {
        lobbyBase_Controller.lobbyPlayerController.ResetLobbyPlayer();
        lobbyBase_Controller.doorController.OpenDoorTween(EnterDuration);
        StartFadeImageAlpha(toOut);
        StartFadeIn();
        UpPanelwhenEnter();
    }

    private void StartFadeImageAlpha(float startTo)
    {
        beforeFAdeOutColor.a = startTo;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
    }

    private void StartFadeOut()
    {
        fadeOutImageObj.GetComponent<Image>().raycastTarget = true;

        isCompletedFadeOut = false;
        var d = LeanTween.value(toIn, toOut, LeaveDuration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeOutTweenComplete);
    }

    private void StartFadeIn()
    {
        fadeOutImageObj.GetComponent<Image>().raycastTarget = false;

        isCompletedFadeIn = false;
        var d = LeanTween.value(toOut, toIn, EnterDuration);
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