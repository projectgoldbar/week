using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class TestGenerator : MonoBehaviour
{
    public List<Transform> monsters;
    public Player player;

    public float time = 0;
    public Text resultText;
    public Transform[] genPont;
    public Button button;

    public InputField genCountInputField;
    public InputField playerHpInputField;

    public Transform pltrs;
    public int genCount = 0;
    public int hp = 0;
    private Stopwatch sw = new Stopwatch();

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (a == null)
        {
            return;
        }
        if (a.GetComponent<Player>().Hp == hp - 1)
        {
            sw.Start();
        }
        time = sw.ElapsedMilliseconds;

        if (a.GetComponent<Player>().Hp == 1)
        {
            GameOver();
        }
    }

    public void GenCountSet()
    {
        var a = int.Parse(genCountInputField.text);
        genCount = a;
    }

    public void PlayerHpSet()
    {
        var a = int.Parse(playerHpInputField.text);
        hp = a;
    }

    [SerializeField]
    public List<Transform> monsterList;

    public GameObject a;

    public void GameStart()
    {
        time = 0;
        sw.Start();

        //a = Instantiate(player.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        a.transform.position = pltrs.position;
        GenMonster(genCount);
        genCountInputField.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        //StartCoroutine(Timer());
        Time.timeScale = 1;
        //for (int i = 0; i < monsterList.Count; i++)
        //{
        //    if (monsterList[i].gameObject.activeSelf == true)
        //    {
        //        monsterList[i].GetComponent<Gallery>().StartCoroutine(monsterList[i].GetComponent<Gallery>().CalculatePath());
        //    }
        //}
    }

    private void GameOver()
    {
        int onZombieCount = 0;
        StopAllCoroutines();
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].gameObject.activeSelf)
            {
                monsterList[i].gameObject.SetActive(false);
                onZombieCount++;
            }
        }
        //player.transform.position = new Vector3(0, 1.14f, 0);
        player.Hp = hp;
        genCountInputField.gameObject.SetActive(false);
        resultText.text = "좀비수 =" + onZombieCount + "버틴시간 = " + time / 1000;

        Time.timeScale = 0;
        genCountInputField.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        sw.Reset();
    }

    public void GenMonster(int count)
    {
        //if (monsterList.Count < count)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        float x = Random.Range(0, 5f);
        //        float z = Random.Range(0, 5f);
        //        var monster = GameObject.Instantiate(monsters[0], genPont[Random.Range(0, genPont.Length)].position + new Vector3(x, 0, z), Quaternion.identity);
        //        monsterList.Add(monster);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        float x = Random.Range(0, 5f);
        //        float z = Random.Range(0, 5f);
        //        monsterList[i].transform.position = genPont[Random.Range(0, genPont.Length)].position + new Vector3(x, 0, z);
        //        monsterList[i].gameObject.SetActive(true);
        //    }
        //}
        StartCoroutine(GenMonster2(count));
    }

    private IEnumerator GenMonster2(int count)
    {
        if (monsterList.Count < count)
        {
            for (int i = 0; i < count; i++)
            {
                float x = Random.Range(0, 5f);
                float z = Random.Range(0, 5f);
                var monster = GameObject.Instantiate(monsters[0], genPont[Random.Range(0, genPont.Length)].position + new Vector3(x, 0, z), Quaternion.identity);

                monsterList.Add(monster);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                float x = Random.Range(0, 5f);
                float z = Random.Range(0, 5f);
                monsterList[i].transform.position = genPont[Random.Range(0, genPont.Length)].position + new Vector3(x, 0, z);
                monsterList[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}