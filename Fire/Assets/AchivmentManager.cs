using UnityEngine;
using System;


public class AchivmentManager : MonoBehaviour
{
    public enum Achivemnt{ tutorial,stage,playtime,playCount,Gold,BoxCount,BoxOpen,Upgrade}
    public Action[] achivments;
    public GooglePlayGPGS gpgs;

    private void Awake()
    {
        achivments = new Action[12] {()=> { }, () => { }, () => { }, () => { },
        ()=> { },()=> { },()=> { },()=> { },()=> { },()=> { },()=> { },()=> { }};
        achivments[0] = gpgs.Starter_AchievementPosting;
        achivments[1] = gpgs.Stage1_Achievement_Open;
        achivments[2] = gpgs.Stage2_Achievement_Open;
        achivments[3] = gpgs.Stage5_Achievement_Open;
        achivments[4] = gpgs.Stage7_Achievement_Open;
        achivments[5] = gpgs.Stage10_Achievement_Open;
        achivments[6] = gpgs.PlayTime_Achievement_Open;
        achivments[7] = gpgs.PlayCount_Achievement_Open;
        achivments[8] = gpgs.PlayGold_Achievement_Open;
        achivments[9] = gpgs.Potion_Achievement_Open;
        achivments[10] = gpgs.Box_Achievement_Open;
        achivments[11] = gpgs.BoxOpen_Achievement_Open;
        achivments[12] = gpgs.Upgrade_Achievement_Open;
    }

    //유저데이타에서 업적배열 받아와서 안한것만 일단 등록


    void CheckProcess()
    {
        int count = 0;
        for (int i = 0; i < UserDataManager.Instance.userData.statPointerIdx.Length; i++)
        {
            count += UserDataManager.Instance.userData.statPointerIdx[i];
        }
        if (count > 50)
        {
            //업적 줄라고 햇는데 실패시 어케 동작하는지 알아봐야함
            achivments[12]();
        }
    }


}
