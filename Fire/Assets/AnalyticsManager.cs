using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public class AnalyticsManager : MonoBehaviour
{
    
    private static AnalyticsManager instance = null;
    public static AnalyticsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(AnalyticsManager)) as AnalyticsManager;
                if (instance == null)
                {
                    Debug.Log("no");
                }

            }
            return instance;
        }
    }


    DateTime startDate = DateTime.Now;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Analytics.CustomEvent("게임시작", new Dictionary<string, object>
        {
            {"닉네임", Social.localUser.userName},
            {"시작시간", startDate}

        });


        //AnalyticsEvent.TutorialStart("Tuto", new Dictionary<string, object>
        //{
        //    {"닉네임", Social.localUser.userName},
        //    {"시작시간", startDate}
        //});


    }

    public void OnApplicationQuit()
    {
        var x = DateTime.Now;
        Analytics.CustomEvent("종료", new Dictionary<string, object>
        {
            { " 종료시간 = " , x},
            { " 플레이시간 = " , TimeCalc() },
            { " 최고스테이지 = ", UserDataManager.Instance.userData.highStage }

        });
    }


    public void TutorialClear()
    {
        Analytics.CustomEvent("튜토리얼", new Dictionary<string, object>
        {
            { "튜토리얼 클리어 Id" , Social.localUser.id}
        });
    }

    public void Stage5Clear()
    {
        Analytics.CustomEvent(" 한번에 스테이지 5 클리어 ", new Dictionary<string, object>
        {
            { $" 한번에 스테이지5 클리어 시간" ,TimeCalc()}
        });
    }


    public void StageClear(int stage)
    {
        Analytics.CustomEvent("스테이지클리어", new Dictionary<string, object>
        {
            { $"스테이지번호" ,stage}
        });
    }

    public void Death(int stage)
    {
        Analytics.CustomEvent("플레이어사망", new Dictionary<string, object>
        {
            { $"스테이지번호" ,stage}
        });
    }

    public void AdsClear()
    {
        Analytics.CustomEvent(" 광고보기 성공 ", new Dictionary<string, object>
        {
            { $" 광고보기 성공한 닉네임 "  , Social.localUser.userName},
            { " 광고보기 성공한 Id " , Social.localUser.id}
        });
    }


    int TimeCalc()
    {
        
        var x = DateTime.Now;
        return (x - startDate).Seconds;

    }


    


}
