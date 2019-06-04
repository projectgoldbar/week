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
    public float maxLiveTime;

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
    public int[] skillLVList = new int[23] 
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0};

    [Header("스킬장착정보")]
    public bool[] skillEquip = new bool[93];
    

    [Header("스킬수집정보")]
    public bool[] skillCollection = new bool[93];


   


    //public bool[] SkillCollection 
    //{
    //    get { return skillCollection; }
    //    set {
    //        skillCollection = value;
    //        CsvEquipPanel.CollectionPanelOnoff();
    //    }
    //}


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
    public float MaxHp;
    [Header("체력감소속도")]
    public float Hpdeceleration;
    [Header("방어력")]
    public float DEF;
    [Header("획득체력")]
    public float HpGain;
    [Header("획득 추가진화포인트")]
    public float Gainevolution;
    [Header("골드 획득량")]
    public float MoneyGain;
    [Header("2배 시간")]
    public float StartRange;
    [Header("스킬몸크기")]
    public float BodySize;
    [Header("스킬지속시간")]
    public float duration;
    [Header("스킬최대치")]
    public float Maximum;

   
    
    [Header("1회성 2배 시간")]
    public float One_Time_StartRange;
    [Header("1회성 골드 획득량")]
    public float One_Time_MoneyGain;
    [Header("1회성 전체 체력 증가")]
    public float One_Time_MaxHpUp;
    [Header("1회성 스킬최대치")]
    public float One_Time_Maximum;
    [Header("1회성 획득체력")]
    public float One_Time_HpGain;





    public UserAbillity() { }

    public UserAbillity(float Maxhp,
                        float Hpdeceleration,
                        float DEF,
                        float HpGain, 
                        float Gainevolution,
                        float MoneyGain,
                        float StartRange ,
                        float bodysize,
                        float duration,
                        float maximum)

    {
        this.MaxHp         = Maxhp;
        this.Hpdeceleration = Hpdeceleration;
        this.DEF            = DEF;
        this.HpGain         = HpGain;
        this.Gainevolution  = Gainevolution;
        this.MoneyGain      = MoneyGain;
        this.StartRange     = StartRange;
        this.BodySize       = bodysize;
        this.duration       = duration;
        this.Maximum        = maximum;
    }

    

}
