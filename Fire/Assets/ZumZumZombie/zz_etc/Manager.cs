using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.SceneManagement;

//[DefaultExecutionOrder(-400)]
public class Manager : MonoBehaviour
{
    public GameObject[] Human;
    public PlayerData playerData;
    public Text liveTime;
    public GameObject gameOverUi;
    public GameObject gamePauseUi;
    public GameObject gameResultUi;
    public GameObject gameClearUi;
    public GameObject itemResultUi;
    public EvolveSystem evolSystem;
    public StageSystem stageSystem;

    public GameObject evolUi;
    public GameObject evolButton1;
    public GameObject evolButton2;
    public GameObject evolButton3;

    public Text timeUi;
    public Text goldUi;

    [Header("게임오버패널")]
    public Text scoreText;

    public Text survivalTimeTxt;
    public Text coinText;
    public Text StageClearText;

    private List<Vector3[]> line;

    public Vector3[] line1;
    public Vector3[] line2;
    public Vector3[] line3;
    public Stopwatch sw = new Stopwatch();
    public float score;//스코어
    public float resultGold;

    public bool viewAd = false;

    public AdmobVideoAd AdsVideo;

    private void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
        AdsVideo = FindObjectOfType<AdmobVideoAd>();
        line = new List<Vector3[]>();
        line.Add(line1);
        line.Add(line2);
        line.Add(line3);

        var setLine = line[Random.Range(0, line.Count)];
        float left = setLine[0].x;
        float right = setLine[1].x;
        float x = Random.Range(left, right);
        playerData.transform.position = new Vector3(x, setLine[0].y, setLine[0].z);
        goldUi.text = playerData.gold.ToString();
        SceneManager.sceneUnloaded += OnSceneEnded;
        GameStart();
        //PlayerSetting();
    }

    private void OnSceneEnded(Scene scene)
    {
        var x = UserDataManager.Instance.userData;
        playerData.gold += resultGold;
        x.Money = Mathf.Round(playerData.gold);
        for (int i = 0; i < playerData.randomBox.Length; i++)
        {
            x.randomBox[i] = playerData.randomBox[i];
        }
        x.playCount++;
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

        evolUi.SetActive(true);
        GamePause();
    }

    public float openTime = 10f;

    private void Update()
    {
        score = Mathf.Floor(sw.ElapsedMilliseconds * 0.001f);
        timeUi.text = "Score " + score;
        if (score > openTime)
        {
            stageSystem.InstanceStageOpen();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameOver();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(TimeToGold());
        }
    }

    private void GamePause()
    {
        Time.timeScale = 0;
        sw.Stop();
    }

    private void GameStart()
    {
        Time.timeScale = 1;
        sw.Start();
    }

    public void GameOver()
    {
        sw.Stop();
        Time.timeScale = 0;
        GameOverSeq();
    }

    private float sec;
    private float min;
    private float hour;

    public void GameOverSeq()
    {
        if (playerData.clearCount > 0)
        {
            if (!gameClearUi.activeSelf)
            {
                gameClearUi.SetActive(true);
            }
        }
        else
        {
            scoreText.text = score.ToString();
            var playTime = score;
            sec = playTime % 60;
            min = playTime / 60 % 60;
            hour = playTime / 3600;
            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
            gameOverUi.SetActive(true);
            StartCoroutine(TimeToGold());
        }
    }

    private IEnumerator TimeToGold()
    {
        //var gold = playerData.gold;
        var gold = resultGold;
        while (sec > 1)
        {
            sec -= 1f;
            gold++;
            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
            coinText.text = gold.ToString();
            yield return null;
        }
        sec = 0f;
        while (min > 1)
        {
            min -= 1f;
            gold += 60f;
            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);
            coinText.text = gold.ToString();

            yield return null;
        }
        min = 0f;
        while (hour > 1)
        {
            hour -= 1f;
            gold += 3600f;

            survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);

            coinText.text = gold.ToString();

            yield return null;
        }
        hour = 0f;
        survivalTimeTxt.text = string.Format("{0:00} : {1:00} : {2:00}", hour, min, sec);

        resultGold = gold;
    }

    public void GameClearUiSeq()
    {
        //파티클실행
        var x = RandomBoxManager.Instance.SkinBoxOpen(UserDataManager.Instance.skinInfos.Length);
        UserDataManager.Instance.userData.gainSkin[x] = true;
        gameClearUi.SetActive(false);
        itemResultUi.SetActive(true);
    }

    public void ItemResultUiseq()
    {
        GameOverSeq();
    }

    private void GameResume()
    {
        sw.Start();
    }

    public void ViewAD()
    {
        //광고보기
        AdsVideo.ShowRewardedAd();
    }

    public void Resume(GameObject ui)
    {
        ui.SetActive(false);
        sw.Start();
    }

    public void GoIntro()
    {
        SceneManager.LoadScene(0);
    }

    public void RewardGold()
    {
        resultGold *= 2;
        coinText.text = resultGold.ToString();
    }
}