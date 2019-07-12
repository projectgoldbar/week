using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewUserData
{
    [Header("")]
    public float playTime;

    public int playCount;
    public int dieCount;

    #region 세이브할목록들

    public int[] statPointerIdx = new int[5] { 1, 1, 1, 1, 1 };
    public bool isTutorialClear = false;


    #region 스코어

    public float highScore = 0f;

    #endregion 스코어

    #region MONEY

    public float Money;

    #endregion MONEY

    #region 스킨관련

    [Header("스킬습득관련")]
    public int[] skillLVList = new int[23]
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0};

    [Header("스킨수집정보")]
    public bool[] gainSkin = new bool[11]
    {true,false,false,false,false,false,false,false,false,false,false };

    [Header("장착스킨정보")]
    public int equipedSkinIdx = 0;

    #endregion 스킨관련

    #region 랜덤박스관련

    public int goldBoxCount = 0;
    public int silverBoxCount = 0;
    public int bronzeBoxCount = 0;

    #endregion 랜덤박스관련

    #region 인앱결제관련

    public bool AdOff = false;
    public bool goldBonus = false;
    public bool pakage = false;

    #endregion 인앱결제관련

    #endregion 세이브할목록들

    #region 인게임플레이어스테이터스

    public int hp;
    public float healHp;
    public float ep;
    public float healEp;
    public float gainMoney;

    //------안쓰는 것들임
    public int df;

    public float decelerationHp;
    public float gainExp;
    public int maxSkillCount;
    public int skillCoolDown;

    #endregion 인게임플레이어스테이터스

    #region 소비아이템구매여부

    [Header("소비아이템구매여부")]
    private int bloodOil = 0;

    private int rubberDuck = 0;

    private int boodooDoll = 0;

    private int twinPye = 0;

    private int bottleHeart = 0;

    #endregion 소비아이템구매여부

    #region 업적클리어현황

    [Header("업적클리어현황")]
    public bool[] achievements;

    #endregion 업적클리어현황
}