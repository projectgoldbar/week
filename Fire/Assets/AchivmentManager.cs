using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class AchivmentManager : MonoBehaviour
{
    public enum Achivemnt { tutorial, stage, playtime, playCount, BoxCount, BoxOpen, Upgrade }
    public Action[] achivments;
    public GooglePlayGPGS gpgs;
    Achivemnt achivemnt = Achivemnt.tutorial;

    private void Awake()
    {
        achivments = new Action[11] {()=> { }, () => { }, () => { }, () => { },
        ()=> { },()=> { },()=> { },()=> { },()=> { },()=> { },()=> { }};
        achivments[0] = gpgs.Starter_AchievementPosting;
        achivments[1] = gpgs.Stage1_Achievement_Open;
        achivments[2] = gpgs.Stage2_Achievement_Open;
        achivments[3] = gpgs.Stage5_Achievement_Open;
        achivments[4] = gpgs.Stage7_Achievement_Open;
        achivments[5] = gpgs.Stage10_Achievement_Open;
        achivments[6] = gpgs.PlayTime_Achievement_Open;
        achivments[7] = gpgs.PlayCount_Achievement_Open;
        achivments[8] = gpgs.Potion_Achievement_Open;
        achivments[9] = gpgs.Box_Achievement_Open;
        achivments[10] = gpgs.BoxOpen_Achievement_Open;

    }

    private void Start()
    {
        var userData = UserDataManager.Instance.userData;
        if (UserDataManager.Instance.userData.isTutorialClear)
        {
            CheckCleared();
        }
    }



    //유저데이타에서 업적배열 받아와서 안한것만 일단 등록
    void CheckCleared()
    {
        var userData = UserDataManager.Instance.userData;
        for (int i = 0; i < userData.achievements.Length; i++)
        {
            if (userData.achievements[i] == true)
            {
                continue;
            }
            else
            {
                CheckProcess(i);
            }
        }
       
    }


    void CheckProcess(int idx)
    {
        switch (idx)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;

            default:
                break;
        }
    }

    bool TutorialCleared()
    {
        if (UserDataManager.Instance.userData.isTutorialClear)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool StageCleared(int stage)
    {
        if (UserDataManager.Instance.userData.highStage >= stage)
        {
            return true;
        }
        else { return false; }
    }
    bool PlayTimeReached()
    {
        if (UserDataManager.Instance.userData.playTime >= 3600f)
        {
            return true;
        }
        else
            return false;
    }

    bool PlayCountReached()
    {
        if (UserDataManager.Instance.userData.playCount >= 100)
        {
            return true;
        }
        else return false;
    }


    bool HealPackCountReached()
    {
        if (UserDataManager.Instance.userData.accumulateHealPack >= 200)
        {
            return true;
        }
        else return false;
    }
    bool BoxCountReached()
    {
        if (UserDataManager.Instance.userData.accumulateBoxCount >= 100)
        {
            return true;
        }
        else return false;
    }

    bool BoxOpenReached()
    {
        if (UserDataManager.Instance.userData.accumulateBoxOpen >= 100)
        {
            return true;
        }
        else return false;
    }
}


