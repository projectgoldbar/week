using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBaseFlowManager : MonoBehaviour
{
    private static LobbyBaseFlowManager instance;

    public static LobbyBaseFlowManager Instace
    {
        get { return instance; }
    }

    public LobbyPlayerController lobbyPlayerController;
    public GameScene gameScene;

    public void OnPlay_Button()
    {
        if (LeanTween.isTweening(gameScene.fadeOutImageObj))
        {
            Debug.Log("_________________fadeOutImageObj   isTweening");
            return;
        }
        FlowBeforePlay();
    }

    public void FlowBeforePlay()
    {
        lobbyPlayerController.FlowBeforePlay();
        gameScene.FlowBeforePlay();
    }
}