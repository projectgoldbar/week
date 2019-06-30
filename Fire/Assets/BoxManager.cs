using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    private NewUserData userData;

    [Header("박스선택버튼")]
    public GameObject bronzeBoxPanel;

    public GameObject silverBoxPanel;
    public GameObject goldBoxPanel;
    public GameObject emptyBoxPanel;

    [Header("등장박스오브젝트")]
    public GameObject bronzeBox;

    public GameObject silverBox;
    public GameObject goldBox;

    [Header("박스오픈버튼스프라이트")]
    public Sprite noAdSprite;

    [Header("박스오픈버튼")]
    public Button boxOpenButton;

    [Header("결과정보창")]
    public GameObject resultInfo;

    public Sprite goldSprite;
    public Sprite skinSprite;
    public Text Description;

    public AdmobVideoAd videoAd;

    public enum OpenState { bronze, silver, gold }

    public OpenState openState;

    private void Start()
    {
        userData = UserDataManager.Instance.userData;
        Refresh();
        videoAd.AdsReward.AddListener(TrowDice);
    }

    /// <summary>
    /// 박스를열기위한셋팅을합니다.
    /// </summary>
    /// <param name="boxType"></param>
    public void BoxSet(BoxType boxType)
    {
        switch (boxType)
        {
            case BoxType.Bronze:
                //등장애니메이션함수연결
                break;

            case BoxType.Gold:
                //등장애니메이션함수연결

                break;

            case BoxType.Silver:
                //등장애니메이션함수연결
                break;

            default:
                break;
        }
    }

    private void Refresh()
    {
        int offcount = 0;
        if (userData.AdOff)
        {
            boxOpenButton.GetComponent<Image>().sprite = noAdSprite;
        }
        bronzeBoxPanel.GetComponentInChildren<Text>().text = userData.bronzeBoxCount.ToString();
        silverBoxPanel.GetComponentInChildren<Text>().text = userData.silverBoxCount.ToString();
        goldBoxPanel.GetComponentInChildren<Text>().text = userData.goldBoxCount.ToString();

        if (userData.bronzeBoxCount <= 0)
        {
            bronzeBoxPanel.SetActive(false);
            offcount++;
        }
        else
        {
            bronzeBoxPanel.SetActive(true);
        }

        if (userData.silverBoxCount <= 0)
        {
            silverBoxPanel.SetActive(false);
            offcount++;
        }
        else
        {
            silverBoxPanel.SetActive(true);
        }

        if (userData.goldBoxCount <= 0)
        {
            goldBoxPanel.SetActive(false);
            offcount++;
        }
        else
        {
            goldBoxPanel.SetActive(true);
        }

        if (offcount == 3)
        {
            emptyBoxPanel.SetActive(true);
        }
        else
        {
            emptyBoxPanel.SetActive(false);
        }
    }

    public void BoxOpen(int boxType)

    {
        if (boxType == 0)
        {
            openState = OpenState.bronze;
            if (!userData.AdOff)
            {
                //광고실행함수연결
                videoAd.ShowRewardedAd();
            }
            else
            {
                TrowDice();
            }
        }
        else if (boxType == 1)
        {
            openState = OpenState.silver;
            if (!userData.AdOff)
            {
                //광고실행함수연결
                videoAd.ShowRewardedAd();
            }
            else
            {
                TrowDice();
            }
        }
        else if (boxType == 2)
        {
            openState = OpenState.gold;

            if (!userData.AdOff)
            {
                //광고실행함수연결
                videoAd.ShowRewardedAd();
            }
            else
            {
                TrowDice();
            }
        }
    }

    public void TrowDice()
    {
        switch (openState)
        {
            case OpenState.bronze:
                var x = Random.Range(0, 101);
                if (x < 95)
                {
                    resultInfo.GetComponent<Image>().sprite = goldSprite;
                    //resultInfo.
                }
                else
                {
                    var skin = FindObjectOfType<SkinSystem>().ThrowRandomSkin();
                    resultInfo.GetComponent<Image>().sprite = skin.skinSprite;
                    skin.isHave = true;
                    UserDataManager.Instance.userData.gainSkin[skin.skinIdx] = true;
                }
                break;

            case OpenState.silver:
                var y = Random.Range(0, 101);
                if (y < 80)
                {
                    resultInfo.GetComponent<Image>().sprite = goldSprite;
                    //resultInfo.
                }
                else
                {
                    var skin = FindObjectOfType<SkinSystem>().ThrowRandomSkin();
                    resultInfo.GetComponent<Image>().sprite = skin.skinSprite;
                    skin.isHave = true;
                    UserDataManager.Instance.userData.gainSkin[skin.skinIdx] = true;
                }
                break;

            case OpenState.gold:
                var z = Random.Range(0, 101);
                if (z < 50)
                {
                    resultInfo.GetComponent<Image>().sprite = goldSprite;
                    //resultInfo.
                }
                else
                {
                    var skin = FindObjectOfType<SkinSystem>().ThrowRandomSkin();
                    resultInfo.GetComponent<Image>().sprite = skin.skinSprite;
                    skin.isHave = true;
                    UserDataManager.Instance.userData.gainSkin[skin.skinIdx] = true;
                }
                break;

            default:
                break;
        }
    }
}