using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //씬넘기는기능
    private GameLevelManager glm;

    private NewMonsterGenerator monsterGenerator;
    public Transform zombie;

    private void Awake()
    {
        glm = GameLevelManager.instance;
        monsterGenerator = glm.monsterGenerator;
        Lv2GenPoints = Lv2GenPoint.GetComponentsInChildren<Transform>();
    }

    public void TutorialGameOver()
    {
        SceneManager.LoadScene("00.Tutorials");
    }

    public void TutorialClear()
    {
        SceneManager.LoadScene("01.intro");
    }

    //몬스터 리젠하는기능

    //플레이어가 보물을 먹엇을 때 동작하는 요소
    public Transform Lv2GenPoint;

    public Transform[] Lv2GenPoints;

    public void RootiedItem()
    {
        glm.stage++;
        glm.StageUp();

        StartCoroutine(Invokee(new WaitForSeconds(3f)));
    }

    private IEnumerator Invokee(WaitForSeconds cooldown)
    {
        for (int i = 0; i < 45; i++)
        {
            GenerateZombie();
            yield return cooldown;
        }
    }

    private void GenerateZombie()
    {
        for (int i = 0; i < 1; i++)
        {
            float x = Random.Range(0, 6f);
            float z = Random.Range(0, 6f);
            var monster = GameObject.Instantiate(zombie, Lv2GenPoints[Random.Range(0, Lv2GenPoints.Length)].position + new Vector3(x, 0, z), Quaternion.identity);
        }
    }
}