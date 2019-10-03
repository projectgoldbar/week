using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-5000)]
public class UserDataManager : MonoBehaviour
{
    public NewUserData userData;
    public UpgradeInfoPanels[] upgradeInfoPanels;
    public SKinInfo[] skinInfos;
    public SkinSystem skinSystem;
    public int value;
    public static UserDataManager Instance;
    public float randomValue;
    public SkinnedMeshRenderer[] EquipSkinReference;
    public AdmobBanner admob;
    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 45;

        userData = new NewUserData();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        randomValue = UnityEngine.Random.Range(1, 796854);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();

        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            //admob = FindObjectOfType<AdmobBanner>();
            //admob.AdbannerInit();

            userData.Money += randomValue;
            userData.Money = Shuffle(userData.Money);

            Time.timeScale = 1f;
            FindObjectOfType<GameScene>().EnterLobby();
            upgradeInfoPanels = GameObject.Find("Content 0-14").GetComponentsInChildren<UpgradeInfoPanels>();
            skinInfos = GameObject.Find("SkinInfoPivot_Contents").GetComponentsInChildren<SKinInfo>();
            skinSystem = FindObjectOfType<SkinSystem>();
            for (int i = 0; i < upgradeInfoPanels.Length; i++)
            {
                upgradeInfoPanels[i].statLevel = userData.statPointerIdx[i];
                upgradeInfoPanels[i].Initiate();
            }
            for (int i = 0; i < skinInfos.Length; i++)
            {
                skinInfos[i].isHave = userData.gainSkin[i];
            }
            skinInfos[userData.equipedSkinIdx].Select();
            RefreshSkin();


            GooglePlayGPGS.Instance.InitProcess();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        var x = userData.Money - randomValue;

        NewSaveSystem.SaveData(userData, x);

        GooglePlayGPGS.Instance.SaveButtonClick();
    }

    private void OnApplicationQuit()
    {
        var x = userData.Money - randomValue;

        NewSaveSystem.SaveData(userData, x);

        GooglePlayGPGS.Instance.SaveButtonClick();

    }

    public void LoadData()
    {
        var data = NewSaveSystem.LoadData();

        if (data != null)
        {
            userData.Money = data.money;
            userData.statPointerIdx = data.statPointerIdx;
            userData.playTime = data.playTime;
            userData.gainSkin = data.gainSkin;
            userData.equipedSkinIdx = data.equipedSkinIdx;
            userData.goldBoxCount = data.goldBox;
            userData.silverBoxCount = data.silberBox;
            userData.bronzeBoxCount = data.bronzeBox;
            userData.AdOff = data.adoff;
            userData.goldBonus = data.goldBouns;
            userData.pakage = data.pakage;
            userData.isTutorialClear = data.isTutorialClear;
            userData.highScore = data.highScore;
            userData.accumulateBoxCount = data.accumulateBoxCount;
            userData.accumulateBoxOpen = data.accumulateBoxOpen;
            userData.accumulateHealPack = data.accumulateHealPack;
            userData.highStage = data.highStage;
            userData.playCount = data.playCount;
            userData.achievements = data.achivements;
        }
    }

    public void Refresh()
    {
        userData.Money = Shuffle(userData.Money);
        for (int i = 0; i < upgradeInfoPanels.Length; i++)
        {
            upgradeInfoPanels[i].statLevel = userData.statPointerIdx[i];
            upgradeInfoPanels[i].Refresh();
        }
    }

    public void RefreshSkin()
    {
        for (int i = 0; i < skinInfos.Length; i++)
        {
            skinInfos[i].Refresh();
        }
    }

    private float Shuffle(float x)
    {
        x -= randomValue;
        randomValue = Random.Range(2, 15329);
        x += randomValue;
        return x;
    }

    public float UnShuffle(float x)
    {
        x = x - randomValue;
        return x;
    }


}