using UnityEngine;
using UnityEngine.UI;
using System;

[DefaultExecutionOrder(-5010)]
public class UpgradeInfoPanels : MonoBehaviour
{
    public STATINDEX statIndex;
    public Image statIconSprite;
    public Text statLevelTxtforDev;//나중에 지우기
    public Text statIndexTxt;
    public Text statValueTxt;
    public Text statPriceTxt;
    private bool firstInit = false;
    private UserDataManager userDataManager;

    public int statLevel = 0;
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
    private void Awake()
    {
        userDataManager = FindObjectOfType<UserDataManager>();

        upgradeStat = new Action[] { () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { } };
        upgradeStat[0] = () => Upgrade00();
        upgradeStat[1] = () => Upgrade01();
        upgradeStat[2] = () => Upgrade02();
        upgradeStat[3] = () => Upgrade03();
        upgradeStat[4] = () => Upgrade04();
        //upgradeStat[5] = () => Upgrade05();
        //upgradeStat[6] = () => Upgrade06();
        //upgradeStat[7] = () => Upgrade07();
        //upgradeStat[8] = () => Upgrade08();
        //upgradeStat[9] = () => Upgrade09();
        //upgradeStat[10] = () => Upgrade10();
        //upgradeStat[11] = () => Upgrade11();
        //upgradeStat[12] = () => Upgrade12();
        //upgradeStat[13] = () => Upgrade13();
        //upgradeStat[14] = () => Upgrade14();
    }

    public void Initiate()
    {
        Refresh();
        firstInit = true;
    }

    public void LevelUp()
    {
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
        if (!firstInit)
        {
            statIndexTxt.text = string.Format("최대 체력 LV : {0}", upgradeStatInfo.level);
            statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            userDataManager.userData.hp = upgradeStatInfo.value;
            userDataManager.userData.statPointerIdx[0] = statLevel;
        }
        else
        {
            if (userDataManager.userData.Money < upgradeStatInfo.price + userDataManager.randomValue)
            {
                Debug.Log("돈부족");
                return;
            }
            else if (statLevel > 80)
            {
                statIndexTxt.text = string.Format("최대 체력 LV : MAX", upgradeStatInfo.level);
                statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            }
            else
            {
                userDataManager.userData.Money -= upgradeStatInfo.price;
                ++statLevel;
                upgradeStatInfo = CSVManager.Instance.GetUpgrade00maxHpCSV(statLevel);
                statIndexTxt.text = string.Format("최대 체력 LV : {0}", upgradeStatInfo.level);
                statValueTxt.text = string.Format("maxHp : {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
                userDataManager.userData.hp = upgradeStatInfo.value;
                userDataManager.userData.statPointerIdx[0] = statLevel;
            }
        }
    }

    private void Upgrade01()
    {
        Upgrade01decelerationHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade01decelerationHpCSV(statLevel);
        if (!firstInit)
        {
            statIndexTxt.text = string.Format("회복력 LV : {0}", upgradeStatInfo.level);
            statValueTxt.text = string.Format("회복력 + {0}%", upgradeStatInfo.value);
            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            userDataManager.userData.healHp = upgradeStatInfo.value;
            userDataManager.userData.statPointerIdx[1] = statLevel;
        }
        else
        {
            if (userDataManager.userData.Money < upgradeStatInfo.price + userDataManager.randomValue)
            {
                Debug.Log("돈부족");
                return;
            }
            else if (statLevel > 9)
            {
                statIndexTxt.text = string.Format("회복력 LV : MAX", upgradeStatInfo.level);
                statValueTxt.text = string.Format("회복력 + {0}%", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            }
            else
            {
                userDataManager.userData.Money -= upgradeStatInfo.price;
                ++statLevel;
                upgradeStatInfo = CSVManager.Instance.GetUpgrade01decelerationHpCSV(statLevel);
                statIndexTxt.text = string.Format("회복력 LV : {0}", upgradeStatInfo.level);
                statValueTxt.text = string.Format("회복력 + {0}%", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
                userDataManager.userData.healHp = upgradeStatInfo.value;
                userDataManager.userData.statPointerIdx[1] = statLevel;
            }
        }
    }

    private void Upgrade02()
    {
        Upgrade02defenseCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade02defenseCSV(statLevel);
        if (!firstInit)
        {
            statIndexTxt.text = string.Format("스태미너 LV : {0}", upgradeStatInfo.level);
            statValueTxt.text = string.Format("최대스태미너 : {0}", upgradeStatInfo.value);
            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            userDataManager.userData.ep = upgradeStatInfo.value;
            userDataManager.userData.statPointerIdx[2] = statLevel;
        }
        else
        {
            if (userDataManager.userData.Money < upgradeStatInfo.price + userDataManager.randomValue)
            {
                Debug.Log("돈부족");
                return;
            }
            else if (statLevel > 25)
            {
                statIndexTxt.text = string.Format("스태미너 LV : MAX", upgradeStatInfo.level);
                statValueTxt.text = string.Format("최대스태미너 : {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            }
            else
            {
                userDataManager.userData.Money -= upgradeStatInfo.price;
                ++statLevel;
                upgradeStatInfo = CSVManager.Instance.GetUpgrade02defenseCSV(statLevel);
                statIndexTxt.text = string.Format("스태미너 LV : {0}", upgradeStatInfo.level);
                statValueTxt.text = string.Format("최대스태미너 : {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
                userDataManager.userData.ep = upgradeStatInfo.value;
                userDataManager.userData.statPointerIdx[2] = statLevel;
            }
        }
    }

    private void Upgrade03()
    {
        Upgrade03gainHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade03gainHpCSV(statLevel);
        if (!firstInit)
        {
            statIndexTxt.text = string.Format("스태미너회복속도LV : {0}", upgradeStatInfo.level);
            statValueTxt.text = string.Format("스태미너회복속도 + {0}%", upgradeStatInfo.value);
            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            userDataManager.userData.healEp = upgradeStatInfo.value;
            userDataManager.userData.statPointerIdx[3] = statLevel;
        }
        else
        {
            if (userDataManager.userData.Money < upgradeStatInfo.price + userDataManager.randomValue)
            {
                Debug.Log("돈부족");
                return;
            }
            else if (statLevel > 10)
            {
                statIndexTxt.text = string.Format("스태미너회복속도LV : MAX", upgradeStatInfo.level);
                statValueTxt.text = string.Format("스태미너회복속도 + {0}%", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            }
            else
            {
                userDataManager.userData.Money -= upgradeStatInfo.price;
                ++statLevel;
                upgradeStatInfo = CSVManager.Instance.GetUpgrade03gainHpCSV(statLevel);
                statIndexTxt.text = string.Format("스태미너회복속도LV : {0}", upgradeStatInfo.level);
                statValueTxt.text = string.Format("스태미너회복속도 + {0}%", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
                userDataManager.userData.healEp = upgradeStatInfo.value;
                userDataManager.userData.statPointerIdx[3] = statLevel;
            }
        }
    }

    private void Upgrade04()
    {
        Upgrade04gainPointCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade04gainPointCSV(statLevel);
        if (!firstInit)
        {
            statIndexTxt.text = string.Format("골드획득 LV : {0}", upgradeStatInfo.level);
            statValueTxt.text = string.Format("골드획득량 + {0}", upgradeStatInfo.value);
            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            userDataManager.userData.gainMoney = upgradeStatInfo.value;
            userDataManager.userData.statPointerIdx[4] = statLevel;
        }
        else
        {
            if (userDataManager.userData.Money < upgradeStatInfo.price + userDataManager.randomValue)
            {
                Debug.Log("돈부족");
                return;
            }
            else if (statLevel > 4)
            {
                statIndexTxt.text = string.Format("골드획득 LV : MAX", upgradeStatInfo.level);
                statValueTxt.text = string.Format("골드획득량 + {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
            }
            else
            {
                userDataManager.userData.Money -= upgradeStatInfo.price;
                ++statLevel;
                upgradeStatInfo = CSVManager.Instance.GetUpgrade04gainPointCSV(statLevel);
                statIndexTxt.text = string.Format("골드획득 LV : {0}", upgradeStatInfo.level);
                statValueTxt.text = string.Format("골드획득량 + {0}", upgradeStatInfo.value);
                statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
                userDataManager.userData.gainMoney = upgradeStatInfo.value;
                userDataManager.userData.statPointerIdx[4] = statLevel;
            }
        }
    }

    //private void Upgrade05()
    //{
    //    Upgrade05gainMoneyCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade05gainMoneyCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("골드획득량 + {0}%", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        userDataManager.userData.gainMoney = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[5] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade05gainMoneyCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("골드획득량 + {0}%", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            userDataManager.userData.gainMoney = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[5] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade06()
    //{
    //    Upgrade06startBonusCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade06startBonusCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("StageKey Lv {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[6] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade06startBonusCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("StageKey Lv {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[6] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade07()
    //{
    //    Upgrade07skillBodyRatioCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade07skillBodyRatioCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("스킬사용시몸크기증가 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[7] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade07skillBodyRatioCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("스킬사용시몸크기증가 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[7] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade08()
    //{
    //    Upgrade08skillDurationCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade08skillDurationCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("스킬지속시간 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        userDataManager.userData.skillCoolDown = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[8] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade08skillDurationCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("스킬지속시간 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            userDataManager.userData.skillCoolDown = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[8] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade09()
    //{
    //    Upgrade09skillMaxCountCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade09skillMaxCountCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("최대스킬갯수 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        userDataManager.userData.maxSkillCount = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[9] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade09skillMaxCountCSV(statLevel);

    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("최대스킬갯수 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            userDataManager.userData.maxSkillCount = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[9] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade10()
    //{
    //    Upgrade10instantStartBonusCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade10instantStartBonusCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("소모품1 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[10] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade10instantStartBonusCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("소모품1 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[10] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade11()
    //{
    //    Upgrade11instantGainMoneyCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade11instantGainMoneyCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("소모품2 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[11] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;

    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade11instantGainMoneyCSV(statLevel);

    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("소모품2 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[11] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade12()
    //{
    //    Upgrade12instantGainMaxHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade12instantGainMaxHpCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("소모품3 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[12] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade12instantGainMaxHpCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("소모품3 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[12] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade13()
    //{
    //    Upgrade13instantSkillMaxCountCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade13instantSkillMaxCountCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("소모품4 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[13] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade13instantSkillMaxCountCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("소모품4 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[13] = statLevel;
    //        }
    //    }
    //}

    //private void Upgrade14()
    //{
    //    Upgrade14instantGainHpCSV upgradeStatInfo = CSVManager.Instance.GetUpgrade14instantGainHpCSV(statLevel);
    //    if (!firstInit)
    //    {
    //        statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //        statValueTxt.text = string.Format("소모품5 {0}", upgradeStatInfo.value);
    //        statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //        maxHpLevel = statLevel;
    //        //userDataManager.userData.hp = upgradeStatInfo.value;
    //        userDataManager.userData.statPointerIdx[14] = statLevel;
    //    }
    //    else
    //    {
    //        if (userDataManager.userData.Money < upgradeStatInfo.price)
    //        {
    //            Debug.Log("돈부족");
    //            return;
    //        }
    //        else
    //        {
    //            userDataManager.userData.Money -= upgradeStatInfo.price;
    //            ++statLevel;
    //            upgradeStatInfo = CSVManager.Instance.GetUpgrade14instantGainHpCSV(statLevel);
    //            statIndexTxt.text = string.Format("statIndex : {0}", upgradeStatInfo.level);
    //            statValueTxt.text = string.Format("소모품5 {0}", upgradeStatInfo.value);
    //            statPriceTxt.text = string.Format("{0}", upgradeStatInfo.price);
    //            maxHpLevel = statLevel;
    //            //userDataManager.userData.hp = upgradeStatInfo.value;
    //            userDataManager.userData.statPointerIdx[14] = statLevel;
    //        }
    //    }
    //}
}