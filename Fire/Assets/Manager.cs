using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

[DefaultExecutionOrder(-400)]
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
    private Stopwatch sw = new Stopwatch();

    public bool viewAd = false;

    private void Awake()
    {
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
    }

    public void Evolution()
    {
        var x = evolSystem.Evolve();
        var a = evolButton1.GetComponentsInChildren<Text>();
        var b = evolButton2.GetComponentsInChildren<Text>();
        var c = evolButton3.GetComponentsInChildren<Text>();

        a[0].text = x[0].name;
        b[0].text = x[1].name;
        c[0].text = x[2].name;
        a[1].text = x[0].description;
        b[1].text = x[1].description;
        c[1].text = x[2].description;

        evolUi.SetActive(true);
        GamePause();
    }

    private void SetPlayer()
    {
    }

    private void Update()
    {
        var a = Mathf.Floor(sw.ElapsedMilliseconds * 0.001f);
        timeUi.text = "생존시간" + a;
    }

    private void GamePause()
    {
        Time.timeScale = 0;
        sw.Stop();
    }

    private void GameStart()
    {
        sw.Start();
    }

    public void GameOver()
    {
        sw.Stop();
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    private void GameResume()
    {
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
}