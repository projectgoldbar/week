using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeInfoPanels : MonoBehaviour
{
    public STATINDEX statIndex;
    public Image statIconSprite;
    public Text statLevelTxtforDev;//나중에 지우기
    public Text statIndexTxt;
    public Text statValueTxt;
    public Text statPriceTxt;

    private int statLevel = 1;
    public Action[] upgradeStat;
    //private int statIndex;
    //private int value;
    //private int price;

    private int maxHpLevel;
    private int upgradeLevel;

    //forDev ㄱ

    public Text maxHpLevelText;
    public Text upgradeLevelText;

    // ㄴ
    private void Start()
    {
        upgradeStat = new Action[] { () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { } };
        upgradeStat[0] = () => Upgrade00();
        upgradeStat[1] = () => Upgrade01();
        upgradeStat[2] = () => Upgrade02();
        upgradeStat[3] = () => Upgrade03();
        upgradeStat[4] = () => Upgrade04();
        upgradeStat[5] = () => Upgrade05();
        upgradeStat[6] = () => Upgrade06();
        upgradeStat[7] = () => Upgrade07();
        upgradeStat[8] = () => Upgrade08();
        upgradeStat[9] = () => Upgrade09();
        upgradeStat[10] = () => Upgrade10();
        upgradeStat[11] = () => Upgrade11();
        upgradeStat[12] = () => Upgrade12();
        upgradeStat[13] = () => Upgrade13();
        upgradeStat[14] = () => Upgrade14();

        Refresh();
    }

    public void LevelUp()
    {
        ++statLevel;//나중에 gamedata에서 해야해!
        Refresh();
    }

    public void Refresh()
    {
        switch (statIndex)
        {
            case STATINDEX._00MAXHP:
                upgradeStat[0]();
                break;

            case STATINDEX._01DECELERATIONHP:
                upgradeStat[1]();
                break;

            case STATINDEX._02DEFENSE:
                upgradeStat[2]();
                break;

            case STATINDEX._03GAINHP:
                upgradeStat[3]();
                break;

            case STATINDEX._04GAINPOINT:
                upgradeStat[4]();
                break;

            case STATINDEX._05GAINMONEY:
                upgradeStat[5]();
                break;

            case STATINDEX._06STARTBONUS:
                upgradeStat[6]();
                break;

            case STATINDEX._07SKILLBODYRATIO:
                upgradeStat[7]();
                break;

            case STATINDEX._08SKILLDURATION:
                upgradeStat[8]();
                break;

            case STATINDEX._09SKILLMAXCOUNT:
                upgradeStat[9]();
                break;

            case STATINDEX._10INSTANTSTARTBONUS:
                upgradeStat[10]();
                break;

            case STATINDEX._11INSTANTGAINMONEY:
                upgradeStat[11]();
                break;

            case STATINDEX._12INSTANTGAINMAXHP:
                upgradeStat[12]();
                break;

            case STATINDEX._13INSTANTSKILLMAXCOUNT:
                upgradeStat[13]();
                break;

            case STATINDEX._14INSTANTGAINHP:
                upgradeStat[14]();
                break;

            case STATINDEX.MAX:
                break;

            default:
                break;
        }
    }

    private void Upgrade00()
    {
        Upgrade00maxHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade00maxHpCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite = (Image)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Image));
        statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
        maxHpLevel = statLevel;
    }

    private void Upgrade01()
    {
        Upgrade01decelerationHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade01decelerationHpCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
        upgradeLevel = statLevel;
    }

    private void Upgrade02()
    {
        Upgrade02defenseCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade02defenseCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //  statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade03()
    {
        Upgrade03gainHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade03gainHpCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade04()
    {
        Upgrade04gainPointCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade04gainPointCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade05()
    {
        Upgrade05gainMoneyCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade05gainMoneyCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade06()
    {
        Upgrade06startBonusCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade06startBonusCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade07()
    {
        Upgrade07skillBodyRatioCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade07skillBodyRatioCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //  statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade08()
    {
        Upgrade08skillDurationCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade08skillDurationCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade09()
    {
        Upgrade09skillMaxCountCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade09skillMaxCountCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade10()
    {
        Upgrade10instantStartBonusCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade10instantStartBonusCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade11()
    {
        Upgrade11instantGainMoneyCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade11instantGainMoneyCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade12()
    {
        Upgrade12instantGainMaxHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade12instantGainMaxHpCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        // statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade13()
    {
        Upgrade13instantSkillMaxCountCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade13instantSkillMaxCountCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }

    private void Upgrade14()
    {
        Upgrade14instantGainHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade14instantGainHpCSV(statLevel);
        statLevelTxtforDev.text = string.Format("레벨 : {0}", statLevel);
        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.statIndex);
        statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
        //statIconSprite.sprite = (Sprite)Resources.Load(string.Format("KM/Image/stat{0}", upgradeStatInfo.statIndex), typeof(Sprite));
    }
}