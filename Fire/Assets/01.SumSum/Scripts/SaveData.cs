using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int[] statPointerIdx;
    public bool[] gainSkin;
    public bool[] achivements;
    public int equipedSkinIdx;
    public float playTime;
    public float money;
    public int goldBox;
    public int silberBox;
    public int bronzeBox;
    public bool adoff;
    public bool goldBouns;
    public bool pakage;
    public bool isTutorialClear;
    public float highScore = 0f;
    public int accumulateBoxCount = 0;
    public int playCount = 0;
    public int highStage = 0;
    public int accumulateHealPack = 0;
    public int accumulateBoxOpen = 0;

    public bool AsOnOff;

    public SaveData()
    {
        statPointerIdx = new int[5] { 1, 1, 1, 1, 1 };
        gainSkin = new bool[11] { true, false, false, false, false, false, false, false, false, false, false };
        achivements = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };

        playTime = 0f;
        money = 0;
        equipedSkinIdx = 0;
        goldBox = 0;
        silberBox = 0;
        bronzeBox = 0;
        adoff = false;
        goldBouns = false;
        pakage = false;
        isTutorialClear = false;
        highScore = 0;
        accumulateBoxCount = 0;
        accumulateHealPack = 0;
        accumulateBoxOpen = 0;
        playCount = 0;
        highStage = 0;

        AsOnOff = true;
    }
}