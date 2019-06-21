﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour
{
    public Vector3[] path;

    private Animator lobbyPlayerAnim;

    private List<Vector3> listPath;

    private void Start()
    {
        lobbyPlayerAnim = GetComponent<Animator>();
        listPath = new List<Vector3>();
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

    public void FlowBeforePlay()
    {
        ResetAnim();
        RunAnim();
        RunMove();
    }

    private void ResetAnim()
    {
        //lobbyPlayerAnim.
    }

    private void RunAnim()
    {
        lobbyPlayerAnim.SetTrigger("LobbyRun");
    }

    private void RunMove()
    {
        if (LeanTween.isTweening(gameObject))
        {
            Debug.Log("tweening");
            return;
        }
        Debug.Log("b");
        LTDescr d = LeanTween.moveSpline(gameObject, listPath.ToArray(), 3.0f).setOrientToPath(true).setEase(LeanTweenType.easeInQuad);
        d.setOnComplete(RunMoveComplete);
    }

    private void RunMoveComplete()
    {
        Debug.Log("End");
    }
}