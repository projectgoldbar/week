using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator lobbyPlayerAnim;

    private void Start()
    {
        lobbyPlayerAnim = GetComponent<Animator>();
    }

    public void FlowBeforePlay()
    {
        RunAnim();
    }

    private void RunAnim()
    {
        //Debug.Log("b");
        //lobbyPlayerAnim.SetBool("Static_b", true);
        //lobbyPlayerAnim.SetFloat("Speed_f", 1.0f);
        lobbyPlayerAnim.SetTrigger("LobbyRun");
    }
}