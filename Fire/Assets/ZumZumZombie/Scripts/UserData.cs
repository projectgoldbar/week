using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    [Header("최대체력")]
    public int MaxHp;
    [Header("체력감소속도")]
    public int Hpdeceleration;
    [Header("방어력")]
    public int DEF;
    [Header("획득체력")]
    public int Hpgain;
    [Header("획득 진화포인트")]
    public int Gainevolution;
    [Header("골드 획득량")]
    public int MoneyGain;
    [Header("시작거리")]
    public int StartRange;

    [Header("")]
    public float playTime;
    public int playCount;
    public int dieCount;
    public int clearCount;
    public float maxRiskLevel;

    #region MONEY
    public int Money;
    #endregion

    #region 강화LV

    [Header("강화")]
    public int hpLV;

    public int clearBonusDNALV;
    public int DNAStorageLV;
    public int ZDNAStorageLV;
    public int bootyLV;

    #endregion 강화LV

    #region 소비아이템구매여부

    [Header("소비아이템구매여부")]
    public int itemSkillCount = 0;
    public int itemSkillCooltime = 0;
    public int itemInteractionCount = 0;

    #endregion 소비아이템구매여부

    #region 업적클리어현황

    [Header("업적클리어현황")]
    public bool[] achievements;

    #endregion 업적클리어현황

    #region 인벤토리관련

    [Header("인벤토리관련")]
    public long dna;
    public bool[] bootyList;

    #endregion 인벤토리관련

    #region 스킬습득관련

    [Header("스킬습득관련")]
    public int[] skillPointList;
    public int[] skillLVList;
    public List<SkillData> getSkill;

    #endregion 스킬습득관련
}


[System.Serializable]
public class SkillData
{
    //만약 검색을 해야한다면? 
    //Index 나 Name으로 검색?
    public int Index;               
    public string Name;
    public string Ability;
    public string Ability2;
    public bool Get;
    //열린것과 닫힌것으로 검색을 해야한다면 Get으로 검색?

    public SkillData()
    {
    }
    public SkillData(int index,string name ,string ability , string ability2,bool get)
    {
        Index = index;
        Name = name;
        Ability = ability;
        Ability2 = ability2;
        Get = get;
    }

}
