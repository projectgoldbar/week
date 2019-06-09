using System.Collections.Generic;
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
    public EvolveSystem evolSystem;

    public GameObject evolUi;
    public GameObject evolButton1;
    public GameObject evolButton2;
    public GameObject evolButton3;

    public Text timeUi;
    public Text goldUi;

    private List<Vector3[]> line;

    public Vector3[] line1;
    public Vector3[] line2;
    public Vector3[] line3;
    public Stopwatch sw = new Stopwatch();
    public float playtime;

    public bool viewAd = false;

    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 45;
        playerData = FindObjectOfType<PlayerData>();
        line = new List<Vector3[]>();
        line.Add(line1);
        line.Add(line2);
        line.Add(line3);

        var setLine = line[Random.Range(0, line.Count)];
        float left = setLine[0].x;
        float right = setLine[1].x;
        float x = Random.Range(left, right);
        playerData.transform.position = new Vector3(x, setLine[0].y, setLine[0].z);

        GameStart();
        //PlayerSetting();
    }

    private void PlayerSetting()
    {
        var userEquipSkillList = UserDataMansger.Instance.userData.skillLVList;
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

    private void SetPlayer()
    {
    }

    private void Update()
    {
        playtime = Mathf.Floor(sw.ElapsedMilliseconds * 0.001f);
        timeUi.text = "생존시간 " + playtime;
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
        EndGameSeq();
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    private void GameResume()
    {
        sw.Start();
    }

    public void ViewAD()
    {
    }

    public void ToMain()
    {
    }

    public void Resume(GameObject ui)
    {
        ui.SetActive(false);
        sw.Start();
    }

    public void EndGameSeq()
    {
        var x = UserDataMansger.Instance;
        x.userData.Money = playerData.gold;
        x.UserDataBinarySave(x.userdataname);
    }

    public void GoIntro()
    {
        SceneManager.LoadScene("01_Intro");
    }
}