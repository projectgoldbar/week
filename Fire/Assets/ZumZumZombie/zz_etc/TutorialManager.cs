using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //씬넘기는기능
    private GameLevelManager glm;

    private NewMonsterGenerator monsterGenerator;
    public GameObject zombie;
    public GameObject zombie2;
    public GameObject zombie3;
    public Transform LV2ZombieSpwanPoint;
    public Transform Lv3ZombieSpwanPoint;
    public Transform Lv4ZombieSpwanPoint;

    private void Awake()
    {
        glm = GameLevelManager.instance;
        monsterGenerator = glm.monsterGenerator;
        Lv2GenPoints = LV2ZombieSpwanPoint.GetComponentsInChildren<Transform>();
        Lv3GenPoints = Lv3ZombieSpwanPoint.GetComponentsInChildren<Transform>();
        Lv4GenPoints = Lv4ZombieSpwanPoint.GetComponentsInChildren<Transform>();
        //sw.Start();
    }

    public void TutorialGameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("00.Tutorials");
    }

    public void TutorialClear()
    {
        Time.timeScale = 1;
        GameLevelManager.instance.tutorialClear = true;
        SceneManager.LoadScene("01.intro");
    }

    //몬스터 리젠하는기능

    //플레이어가 보물을 먹엇을 때 동작하는 요소
    public Transform Lv2GenPoint;

    public Transform[] Lv2GenPoints;
    public Transform[] Lv3GenPoints;
    public Transform[] Lv4GenPoints;

    public void RootiedItem()
    {
        glm.stage++;
        glm.StageUp();
        ZombieSpwan(1);
        //StartCoroutine(Invokee(new WaitForSeconds(1f)));
    }

    public void ZombieSpwan(int lv)
    {
        switch (lv)
        {
            case 1:
                StartCoroutine(Invokee(new WaitForSeconds(0.5f), 25, Lv2GenPoints, zombie));
                break;

            case 2:
                StartCoroutine(Invokee(new WaitForSeconds(0.5f), 6, Lv3GenPoints, zombie2));
                break;

            case 3:
                StartCoroutine(Invokee(new WaitForSeconds(0.5f), 3, Lv3GenPoints, zombie3));
                break;

            default:
                break;
        }
    }

    private bool ok = true;

    private IEnumerator Invokee(WaitForSeconds cooldown, int count, Transform[] trs, GameObject zombie)
    {
        for (int i = 0; i < count; i++)
        {
            GenerateZombie(trs, zombie);
            yield return cooldown;
        }
        if (ok == true)
        {
            ZombieSpwan(2);
        }
        else
        {
            ZombieSpwan(3);
            ok = true;
        }
        ok = false;

        yield break;
    }

    private void GenerateZombie(Transform[] trs, GameObject zombie)
    {
        for (int i = 0; i < 1; i++)
        {
            float x = Random.Range(0, 6f);
            float z = Random.Range(0, 6f);
            var monster = Instantiate(zombie, trs[Random.Range(1, trs.Length)].position + new Vector3(x, 0, z), Quaternion.identity);
        }
    }
}