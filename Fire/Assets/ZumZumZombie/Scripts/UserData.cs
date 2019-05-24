using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{

    public UserAbillity userAbillity;


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
    int maxHp = 0;

    #endregion 강화LV

    #region 소비아이템구매여부

    [Header("소비아이템구매여부")]
    int bloodOil = 0;
    int rubberDuck = 0;
    int boodooDoll = 0;
    int twinPye = 0;
    int bottleHeart = 0;


    #endregion 소비아이템구매여부

    #region 업적클리어현황

    [Header("업적클리어현황")]
    public bool[] achievements;

    #endregion 업적클리어현황

    #region 모자관련

    

    [Header("스킬습득관련")]
    public int[] skillLVList;

   

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

public class GameSkill
{
    public int SkillIndex;
    
    public int SkillCount;

    public int Ability;

    public GameSkill() { }

    public GameSkill(int skillIndex, int skillCount, int ability)
    {
        this.SkillIndex = skillIndex;
        this.SkillCount = skillCount;
        this.Ability = ability;
    }
}

[System.Serializable]
public class UserAbillity
{
    [Header("최대체력")]
    public int MaxHp;
    [Header("체력감소속도")]
    public int Hpdeceleration;
    [Header("방어력")]
    public int DEF;
    [Header("획득체력")]
    public int HpGain;
    [Header("획득 추가진화포인트")]
    public int Gainevolution;
    [Header("골드 획득량")]
    public int MoneyGain;
    [Header("2배 시간")]
    public int StartRange;

    public int _MaxHp{ get { return MaxHp; }
        set
        {
            MaxHp = value;
            UnityEngine.MonoBehaviour.FindObjectOfType<PlayerData>().maxhp = MaxHp;
        }
    }

    public UserAbillity() { }

    public UserAbillity(int Maxhp,
                     int Hpdeceleration,
                     int DEF,
                     int HpGain, 
                     int Gainevolution,
                     int MoneyGain,
                     int StartRange )
    {
        this._MaxHp = Maxhp;
        this.Hpdeceleration = Hpdeceleration;
        this.DEF = DEF;
        this.HpGain = HpGain;
        this.Gainevolution = Gainevolution;
        this.MoneyGain = MoneyGain;
        this.StartRange = StartRange;
    }

}
