using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GoldBox : MonoBehaviour
{
    public int RewardLv =1;                         //박스 보상렙 *3~6~9~
    public int BoxLv =1;    

    public bool Opened = false;                    //박스 오픈여부

    public int min = 50;
    public int max = 100;
    private int gold;                            //박스 오픈 후 얻어야 할 골드

   
    public int Gold
    {
        get => gold;
        set {
            gold = Random.Range(min * BoxLv, max * BoxLv);
        }
    }

    private void Awake()
    {
        Gold = 1;
        Debug.Log(Gold);
    }


    //public void BoxOpen()
    //{
    //    Opened = true;
    //    rewardInfo.OpenedRewardLv = RewardLv;
    //    rewardInfo.RewardGold = Gold;
    //}
}