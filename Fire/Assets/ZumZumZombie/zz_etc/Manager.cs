using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[DefaultExecutionOrder(-400)]
public class Manager : MonoBehaviour
{
    public PlayerData playerData;
    public Text liveTime;
    public GameObject gameOverUi;
    public EvolveSystem evolSystem;
    public StageManager stageManager;
    public GameObject evolUi;
    public GameObject evolButton1;
    public GameObject evolButton2;
    public GameObject evolButton3;
    public GameObject adsButton;
    public GameObject nomalCoinText;
    public GameObject adsCoinText;

    public Text timeUi;
    public Text goldUi;
    public Text boxCountUI;

    public UITweenEffectManager uITweenEffectManager;

    [Header("게임오버패널")]
    public Text scoreText;

    public Text survivalTimeTxt;
    public Text coinText;

    public Transform[] spwanPoints;

    public IEnumerator sw;

    private int StageLv = 0;

    //스코어
    public float score;

    public float resultGold;

    public bool viewAd = false;

    public bool isPause = false;

    public AdmobVideoAd AdsVideo;

    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 45;
        playerData = FindObjectOfType<PlayerData>();

        AdsVideo = FindObjectOfType<AdmobVideoAd>();
        uITweenEffectManager = FindObjectOfType<UITweenEffectManager>();

        if (!playerData.isTutirial)
        {
            playerData.transform.position = spwanPoints[Random.Range(0, spwanPoints.Length)].position;
        }
        //goldUi.text = playerData.Gold.ToString();
        SceneManager.sceneUnloaded += OnSceneEnded;
        //Time.timeScale = 0;
        FindObjectOfType<UITweenEffectManager>().EnterInGame();
        sw = ScoreUp();
        GameStart();
        Invoke("PlayerDataOn", 1f);
        //PlayerSetting();
    }

    private void PlayerDataOn()
    {
        playerData.enabled = true;
    }

    private void OnSceneEnded(Scene scene)
    {
        var x = UserDataManager.Instance.userData;

        playerData.score = score;
        if (score > x.highScore)
        {
            x.highScore = score;
        }

        if (x.highStage < stageManager.currentStageLV)
        {
            x.highStage = stageManager.currentStageLV;
        }
        x.Money += resultGold;

        x.playCount++;
        x.goldBoxCount += playerData.goldBoxCount;
        x.silverBoxCount += playerData.silverBoxCount;
        x.bronzeBoxCount += playerData.bronzeBoxCount;
        x.accumulateBoxCount += playerData.goldBoxCount + playerData.silverBoxCount + playerData.bronzeBoxCount;
        x.accumulateHealPack += playerData.addHealPackCount;
        x.playTime += score - (stageManager.currentStageLV * 4000);

        SceneManager.sceneUnloaded -= OnSceneEnded;
    }

    private void PlayerSetting()
    {
        var userEquipSkillList = UserDataManager.Instance.userData.skillLVList;
        for (int i = 0; i < userEquipSkillList.Length; i++)
        {
            playerData.evolveLvData[i] = userEquipSkillList[i];
        }
    }

    public void Evolution()
    {
        var x = evolSystem.Evolve();
        var a = evolButton1.GetComponentsInChildren<Text>();
        var aimage = evolButton1.GetComponent<Image>();
        var b = evolButton2.GetComponentsInChildren<Text>();
        var bimage = evolButton1.GetComponent<Image>();
        var c = evolButton3.GetComponentsInChildren<Text>();
        var cimage = evolButton1.GetComponent<Image>();

        evolButton1.GetComponent<ChoiceEvolve>().evolve = x[0];
        evolButton2.GetComponent<ChoiceEvolve>().evolve = x[1];
        evolButton3.GetComponent<ChoiceEvolve>().evolve = x[2];
        a[0].text = x[0].name;
        b[0].text = x[1].name;
        c[0].text = x[2].name;
        a[1].text = x[0].description;
        b[1].text = x[1].description;
        c[1].text = x[2].description;
        //aimage.sprite = x[0].sprite;
        //bimage.sprite = x[1].sprite;
        //cimage.sprite = x[2].sprite;

        GamePause();
        evolUi.SetActive(true);
        //playerController.SetActive(false);
    }

    private IEnumerator

     ScoreUp()
    {
        WaitForSeconds oneSeconed = new WaitForSeconds(1f);
        while (true)
        {
            score += 1f;
            yield return oneSeconed;
        }
    }

    private void Update()
    {
        //score = Mathf.Floor(sw.ElapsedMilliseconds * 0.001f);

        timeUi.text = "Score " + score;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            score += 4000f;
        }
    }

    public void GamePause()
    {
        isPause = false;
        StopCoroutine(sw);
        Time.timeScale = 0;
    }

    public void OnGamePause()
    {
        GamePause();
        uITweenEffectManager.PauseGameOpen();
    }

    public void OffGamePause()
    {
        GameResume();
        uITweenEffectManager.PauseGameClose();
    }

    public void GameStart()
    {
        StartCoroutine(sw);

        Time.timeScale = 1;
    }

    public void LateGameOver()
    {
        Invoke("GameOver", 3f);
    }

    public void GameOver()
    {
        StopCoroutine(sw);
        //Time.timeScale = 0;
        FindObjectOfType<PlayerMove>().speed = 0f;
        GameOverSeq();
    }

    private float sec;
    private float min;
    private float hour;

    public void GameOverSeq()
    {
        resultGold = playerData.Gold - UserDataManager.Instance.userData.Money;
        scoreText.text = score.ToString();
        var playTime = score - (4000f * stageManager.currentStageLV);
        if (UserDataManager.Instance.userData.AdOff)
        {
            resultGold *= 2;
            adsButton.SetActive(false);
        }
        sec = playTime % 60;
        min = playTime / 60 % 60;
        hour = playTime / 3600;
        survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
        coinText.text = resultGold.ToString();
        boxCountUI.text = FindObjectOfType<StageManager>().currentStageLV.ToString();
        gameOverUi.SetActive(true);
        SoundManager.Instance.PlaySoundSFX("GAMEEND");
        Invoke("GameStop", 2f);
        //StartCoroutine(TimeToGold());
    }

    private void GameStop()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator TimeToGold()
    {
        WaitForSeconds waitsec = new WaitForSeconds(0.05f);
        var gold = resultGold;
        while (sec > 1)
        {
            sec -= 1f;
            gold++;
            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
            coinText.text = gold.ToString();
            yield return waitsec;
        }
        sec = 0f;
        while (min > 1)
        {
            min -= 1f;
            gold += 60f;
            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
            coinText.text = gold.ToString();

            yield return waitsec;
        }
        min = 0f;
        while (hour > 1)
        {
            hour -= 1f;
            gold += 3600f;

            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);

            coinText.text = gold.ToString();

            yield return waitsec;
        }
        hour = 0f;
        survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);

        resultGold = gold;
    }

    //public void GameClearUiSeq()
    //{
    //    //파티클실행
    //    var x = RandomBoxManager.Instance.SkinBoxOpen(UserDataManager.Instance.skinInfos.Length);
    //    UserDataManager.Instance.userData.gainSkin[x] = true;
    //    gameClearUi.SetActive(false);
    //    itemResultUi.SetActive(true);
    //}

    public void ItemResultUiseq()
    {
        GameOverSeq();
    }

    public void GameResume()
    {
        StartCoroutine(sw);
        Time.timeScale = 1;
        isPause = true;
    }

    public void ViewAD()
    {
        //광고보기
        AdsVideo.ShowRewardedAd();
    }

    public void Resume(GameObject ui)
    {
        ui.SetActive(false);
        StartCoroutine(sw);
    }

    public void GoIntro()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RewardGold()
    {
        resultGold *= 2;
        coinText.text = resultGold.ToString();
        adsButton.SetActive(false);
        nomalCoinText.SetActive(false);
        adsCoinText.SetActive(true);
    }
}