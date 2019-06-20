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

    public void OnPlay_Button()
    {
        FlowBeforePlay();
    }

    public void FlowBeforePlay()
    {
        Debug.Log("a");
        lobbyPlayerController.FlowBeforePlay();
    }
}