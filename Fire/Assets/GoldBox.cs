using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GoldBox : MonoBehaviour
{
    public  int  RewardLv;                          //박스 보상렙 *3~6~9~
    private  int  Gold;                             //박스 오픈 후 얻어야 할 골드

    public  bool Opened = false;                    //박스 오픈여부

    public int min;
    public int max;

    //public GoldBox()
    //{
    //    Gold = Random.Range(min, max);              
    //}

    //public void BoxOpen()
    //{
    //    Opened = true;
    //    rewardInfo.OpenedRewardLv = RewardLv;
    //    rewardInfo.RewardGold = Gold;
    //}


}