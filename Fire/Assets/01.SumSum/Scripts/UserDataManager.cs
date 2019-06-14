using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5000)]
public class UserDataManager : MonoBehaviour
{
    public NewUserData userData;
    public UpgradeInfoPanels[] upgradeInfoPanels;
    public SKinInfo[] skinInfos;
    public int value;
    public static UserDataManager Instance;

    private void Awake()
    {
        userData = new NewUserData();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

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
            upgradeInfoPanels = GameObject.Find("Content 0-14").GetComponentsInChildren<UpgradeInfoPanels>();
            skinInfos = GameObject.Find("SkinInfoPivot").GetComponentsInChildren<SKinInfo>();
            for (int i = 0; i < upgradeInfoPanels.Length; i++)
            {
                upgradeInfoPanels[i].statLevel = userData.statPointerIdx[i];
                upgradeInfoPanels[i].Initiate();
            }
            for (int i = 0; i < skinInfos.Length; i++)
            {
                skinInfos[i].isHave = userData.gainSkin[i];
            }
            skinInfos[userData.equipedSkinIdx].isEquiped = true;
            RefreshSkin();
        }
    }

    private void OnApplicationQuit()
    {
        NewSaveSystem.SaveData(userData);
    }

    public void LoadData()
    {
        var data = NewSaveSystem.LoadData();
        userData.Money = data.money;
        userData.statPointerIdx = data.statPointerIdx;
        userData.playTime = data.playTime;
        userData.gainSkin = data.gainSkin;
        userData.equipedSkinIdx = data.equipedSkinIdx;
    }

    public void Refresh()
    {
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            NewSaveSystem.SaveData(userData);
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
        else if (Input.GetKeyDown(KeyCode.M))
        {
            userData.Money = 50;
            NewSaveSystem.SaveData(userData);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < upgradeInfoPanels.Length; i++)
            {
                upgradeInfoPanels[i].Initiate();
            }
        }
    }
}