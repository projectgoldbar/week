using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInfo : MonoBehaviour
{

    public int OpenedRewardLv;

    private int rewardgold;
    public int RewardGold
    {
        get { return rewardgold; }
        set
        {
            rewardgold = value;
            Debug.Log($"박스 얻음!! 박스에 저장된 골드 {rewardgold}$");
            //RewardGoldText.text = rewardgold.Tostring();
        }
    }

    public GoldBox AddBox = null;


    public void AdsReward()
    {

    }

    //public void AdsReward()
    //{
    //    RewardGold = (Gold * RewardLv);
    //    rewardInfo.RewardGold = RewardGold;
    //    Gold = rewardInfo.RewardGold;
    //}

}
