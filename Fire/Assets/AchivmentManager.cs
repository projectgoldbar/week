using UnityEngine;
using System.Collections.Generic;
using System;

public class AchivmentManager : MonoBehaviour
{
    public Action[] gpgsAchivmentCheck;

    private void Awake()
    {
        gpgsAchivmentCheck = new Action[13]
        {
            () => { },() => { },
            () => { },() => { },
            () => { },() => { },
            () => { },() => { },
            () => { },() => { },
            () => { },() => { },
            () => { }
        };
    }

    private void Start()
    {
        gpgsAchivmentCheck[0] = GooglePlayGPGS.Instance.Starter_AchievementPosting;
        gpgsAchivmentCheck[1] = GooglePlayGPGS.Instance.Stage1_Achievement_Open;
        gpgsAchivmentCheck[2] = GooglePlayGPGS.Instance.Stage2_Achievement_Open;
        gpgsAchivmentCheck[3] = GooglePlayGPGS.Instance.Stage5_Achievement_Open;
        gpgsAchivmentCheck[4] = GooglePlayGPGS.Instance.Stage7_Achievement_Open;
        gpgsAchivmentCheck[5] = GooglePlayGPGS.Instance.Stage10_Achievement_Open;
        gpgsAchivmentCheck[6] = GooglePlayGPGS.Instance.PlayTime_Achievement_Open;
        gpgsAchivmentCheck[7] = GooglePlayGPGS.Instance.PlayCount_Achievement_Open;
        gpgsAchivmentCheck[8] = GooglePlayGPGS.Instance.Potion_Achievement_Open;
        gpgsAchivmentCheck[9] = GooglePlayGPGS.Instance.Box_Achievement_Open;
        gpgsAchivmentCheck[10] = GooglePlayGPGS.Instance.Box50_Achievement_Open;
        gpgsAchivmentCheck[11] = GooglePlayGPGS.Instance.Box150_Achievement_Open;
        gpgsAchivmentCheck[12] = GooglePlayGPGS.Instance.Box200_Achievement_Open;
        var userData = UserDataManager.Instance.userData;
        if (UserDataManager.Instance.userData.isTutorialClear)
        {
            CheckCleared();
        }
    }

    //유저데이타에서 업적배열 받아와서 안한것만 실행
    private void CheckCleared()
    {
        List<int> cleardAchivement = new List<int>();
        var userData = UserDataManager.Instance.userData;
        for (int i = 0; i < userData.achievements.Length; i++)
        {
            if (userData.achievements[i] == true)
            {
                continue;
            }
            else
            {
                if (CheckProcess(i))
                {
                    cleardAchivement.Add(i);
                }
            }
        }
        for (int i = 0; i < cleardAchivement.Count; i++)
        {
            gpgsAchivmentCheck[cleardAchivement[i]]();
        }
    }

    private bool CheckProcess(int idx)
    {
        bool returnValue = false;
        switch (idx)
        {
            case 0:
                returnValue = TutorialCleared();

                break;

            case 1:
                returnValue = StageCleared(1);
                break;

            case 2:
                returnValue = StageCleared(2);

                break;

            case 3:
                returnValue = StageCleared(5);

                break;

            case 4:
                returnValue = StageCleared(7);

                break;

            case 5:
                returnValue = StageCleared(10);

                break;

            case 6:
                returnValue = PlayTimeReached();
                break;

            case 7:
                returnValue = PlayCountReached();
                break;

            case 8:
                returnValue = HealPackCountReached();
                break;

            case 9:
                returnValue = BoxCountReached(100);
                break;

            case 10:
                returnValue = BoxCountReached(50);
                break;

            case 11:
                returnValue = BoxCountReached(150);
                break;

            case 12:
                returnValue = BoxCountReached(200);
                break;

            default:
                break;
        }
        return returnValue;
    }

    private bool TutorialCleared()
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

    private bool StageCleared(int stage)
    {
        if (UserDataManager.Instance.userData.highStage >= stage)
        {
            if (stage == 5)
            {
                AnalyticsManager.Instance.Stage5Clear();
            }
            return true;
        }
        else { return false; }
    }

    private bool PlayTimeReached()
    {
        if (UserDataManager.Instance.userData.playTime >= 3600f)
        {
            return true;
        }
        else
            return false;
    }

    private bool PlayCountReached()
    {
        if (UserDataManager.Instance.userData.playCount >= 100)
        {
            return true;
        }
        else return false;
    }

    private bool HealPackCountReached()
    {
        if (UserDataManager.Instance.userData.accumulateHealPack >= 200)
        {
            return true;
        }
        else return false;
    }

    private bool BoxCountReached(int x)
    {
        if (UserDataManager.Instance.userData.accumulateBoxCount >= x)
        {
            return true;
        }
        else return false;
    }
}