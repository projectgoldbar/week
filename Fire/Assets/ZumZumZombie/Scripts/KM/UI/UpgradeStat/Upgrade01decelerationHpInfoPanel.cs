using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade01decelerationHpInfoPanel : MonoBehaviour
{
    public Image statIconSprite;
    public Text statLevelTxtforDev;//나중에 지우기
    public Text statNameTxt;
    public Text statValueTxt;
    public Text statPriceTxt;

    private int statLevel = 1;

    //private int statIndex;
    //private int value;
    //private int price;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        Upgrade01decelerationHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade01decelerationHpCSV(statLevel);

        statNameTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    }

    public void LevelUp()
    {
        ++statLevel;//나중에 gamedata에서 해야해!
        Refresh();
    }
}