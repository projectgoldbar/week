using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInfo : MonoBehaviour
{
    public static int ComboIndex;

    private int rewardgold;

    public int RewardGold
    {
        get { return rewardgold; }
        set
        {
            rewardgold = value;
        }
    }

    public GoldBox AddBox = null;

    public AdmobVideoAd videoAd = null;

    public Ads UnityADS = null;

    //광고제거 여부 bool (저장용도) 저장하는 곳에 연동시킴
    public static bool AdsDeleted = false;

    private void Awake()
    {
        videoAd.AdsReward.AddListener(Ads_success);
        videoAd.UnityAdsReward.AddListener(UnityADS.ShowAD);
        videoAd.AdsFail.AddListener(Ads_fail);

        UnityADS.Finished.AddListener(Ads_success);
        UnityADS.Failed.AddListener(Ads_fail);
    }

    #region 박스 여는방법 선택

    //광고클릭 (콤보박스열기)
    public void ComboBoxOpen()
    {
        Debug.Log("콤보박스 열기");
        //AddBox = UserDataManager.Instance.userData.goldbox[0];
        Debug.Log("보상골드 = 박스골드");
        RewardGold = AddBox.Gold;

        if (!AdsDeleted)
        {
            videoAd.ShowRewardedAd();
            Debug.Log("영상시청");
        }
        else
        {
            Debug.Log("광고제거성공");
            Ads_success();
        }
    }

    //박스열기
    public void BoxOpen()
    {
        Debug.Log("그냥열기");
        //플레이어한테 돈 넘겨주기
        {
            AddMoney(RewardGold);
        }
        //OBJ박스 제거
        {
            DeleteBox();
        }
    }

    #endregion 박스 여는방법 선택

    //Ads전용
    public void Ads_success()
    {
        Debug.Log("영상시청 성공 / ");
        int Gold = 0;
        //콤보++
        {
            ComboIndex++;
        }
        //콤보수에 따른 돈계산
        {
            Debug.Log("보상 계산");
            //          //3 * 콤보인덱스  =  3 , 6 , 9 ~
            Gold = RewardGold * (AddBox.RewardLv * ComboIndex);
        }
        //플레이어한테 돈 넘겨주기
        {
            AddMoney(Gold);
        }
        //OBJ박스제거
        {
            DeleteBox();
        }

        AddBox = null;
    }

    public void Ads_fail()
    {
        Debug.Log("영상시청 실패");
        Debug.Log("다시 박스리스트에Add 시키고 정렬");
        //UserDataManager.Instance.userData.goldbox.Add(AddBox);
        //UserDataManager.Instance.userData.goldbox.Sort();
        AddBox = null;
    }

    public void AddMoney(int money)
    {
        Debug.Log("플레이어에 보상돈 전달");
        //플레이어 돈 += money;
        //UserDataManager.Instance.userData.Money += money;
    }

    public void DeleteBox()
    {
        Debug.Log("OBJ박스 삭제");
        //UserDataManager.Instance.userData.goldbox.RemoveAt(0);
        // Destroy(AddBox, 0.1f);
    }
}