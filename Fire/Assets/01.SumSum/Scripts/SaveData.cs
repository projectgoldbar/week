using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int[] statPointerIdx;
    public bool[] gainSkin;
    public int equipedSkinIdx;
    public float playTime;
    public float money;
    public int goldBox;
    public int silberBox;
    public int bronzeBox;
    public bool adoff;

    public SaveData()
    {
        statPointerIdx = new int[15] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        gainSkin = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        playTime = 0f;
        money = 0;
        equipedSkinIdx = 0;
        goldBox = 0;
        silberBox = 0;
        bronzeBox = 0;
        adoff = false;
    }
}