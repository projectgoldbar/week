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

    private void Start()
    {
        userData = UserDataManager.Instance.userData;
        Refresh();
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

    public void BoxOpen(BoxType boxType)
    {
        switch (boxType)
        {
            case BoxType.Bronze:
                if (!userData.AdOff)
                {
                    //광고실행함수연결
                }

                break;

            case BoxType.Gold:
                if (!userData.AdOff)
                {
                    //광고실행함수연결
                }
                break;

            case BoxType.Silver:
                if (!userData.AdOff)
                {
                    //광고실행함수연결
                }
                break;

            default:

                break;
        }
    }

    public void TrowDice()
    {
        var x = Random.Range(0, 101);
        if (x > 94)
        {
            resultInfo.GetComponent<Image>().sprite = goldSprite;
            //resultInfo.
        }
        else
        {
            resultInfo.GetComponent<Image>().sprite = skinSprite;
        }
    }
}