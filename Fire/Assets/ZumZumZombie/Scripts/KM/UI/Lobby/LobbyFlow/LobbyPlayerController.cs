using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour
{
    public Vector3[] path;

    private Animator lobbyPlayerAnim;
    private List<Vector3> listPath;

    private Vector3 lobbyPlayerPos;
    private Quaternion lobbyPlayerRotQ;

    private void Awake()
    {
        lobbyPlayerAnim = GetComponent<Animator>();
        listPath = new List<Vector3>();
        lobbyPlayerPos = transform.position;
        lobbyPlayerRotQ = transform.rotation;
        SettingPaths();
    }

    private void SettingPaths()
    {
        listPath.Add(transform.position);
        listPath.Add(transform.position);
        for (int i = 0; i < path.Length; ++i)
        {
            listPath.Add(path[i]);
        }
        listPath.Add(path[path.Length - 1]);
    }

    public void ResetLobbyPlayer()
    {
        IdleAnim();
        IdleTrans();
    }

    private void IdleTrans()
    {
        gameObject.transform.position = lobbyPlayerPos;
        gameObject.transform.rotation = lobbyPlayerRotQ;
    }

    public void RunningAnim()
    {
        if (LeanTween.isTweening(gameObject))
        {
            Debug.Log("tweening _RunningAnim");
            return;
        }

        ResetLobbyPlayer();
        RunAnim();
        RunMove();
    }

    private void IdleAnim()
    {
        lobbyPlayerAnim.SetBool("LobbyRun", false);
    }

    private void RunAnim()
    {
        lobbyPlayerAnim.SetBool("LobbyRun", true);
    }

    private void RunMove()
    {
        LTDescr d = LeanTween.moveSpline(gameObject, listPath.ToArray(), 3.0f).setOrientToPath(true).setEase(LeanTweenType.easeInQuad);
        d.setOnComplete(RunMoveComplete);
    }

    private void RunMoveComplete()
    {
        Debug.Log("RunEnd");
    }
}