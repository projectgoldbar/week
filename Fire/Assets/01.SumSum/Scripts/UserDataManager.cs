﻿using UnityEngine;
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
    public Text debugText;
    public float randomValue;
    public SkinnedMeshRenderer[] EquipSkinReference;

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

    private void Start()
    {
        //for (int i = 0; i < upgradeInfoPanels.Length; i++)
        //{
        //    upgradeInfoPanels[i].Initiate();
        //}
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            Debug.Log("최초골드금액  " + userData.Money);
            userData.Money += randomValue;
            Debug.Log("셔플전금액  " + userData.Money);
            userData.Money = Shuffle(userData.Money);
            Debug.Log("셔플후금액  " + userData.Money);

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
        }
    }

    private void OnApplicationPause(bool pause)
    {
        var x = userData.Money - randomValue;
        NewSaveSystem.SaveData(userData, x);
    }

    private void OnApplicationQuit()
    {
        var x = userData.Money - randomValue;
        NewSaveSystem.SaveData(userData, x);
    }

    public void LoadData()
    {
        debugText.text = "로드들어옴";
        var data = NewSaveSystem.LoadData();
        debugText.text = "데이타로드완료";

        if (data != null)
        {
            debugText.text = "데이타가 잘읽혔음";

            userData.Money = data.money;
            userData.statPointerIdx = data.statPointerIdx;
            userData.playTime = data.playTime;
            userData.gainSkin = data.gainSkin;
            userData.equipedSkinIdx = data.equipedSkinIdx;
            userData.goldBoxCount = data.goldBox;
            userData.silverBoxCount = data.silberBox;
            userData.bronzeBoxCount = data.bronzeBox;
            userData.AdOff = data.adoff;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();

            Debug.Log("돈" + userData.Money);
            Debug.Log("플레이타임" + userData.playTime);
            Debug.Log("장착스킨" + userData.equipedSkinIdx);
            int skinCount = 0;
            for (int i = 0; i < userData.gainSkin.Length; i++)
            {
                if (userData.gainSkin[i])
                {
                    skinCount++;
                }
            }
            Debug.Log("가진스킨개수" + skinCount);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < upgradeInfoPanels.Length; i++)
            {
                upgradeInfoPanels[i].Initiate();
                SceneManager.LoadScene(1);
            }
        }
    }
}