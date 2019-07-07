using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Evolve
{
    public string name;
    public string description;
    public Sprite sprite;
    public int idx = 0;
    public int lv = 0;
}

public class EvolveSystem : MonoBehaviour
{
    public Evolve[] evolveIdx;

    public List<Action> evolveFunc = new List<Action>();

    private void Awake()
    {
        evolveFunc.Add(() => Resiliencepeace());//0
        evolveFunc.Add(() => Beanworm());//1
        evolveFunc.Add(() => GoldWorm());//2
        evolveFunc.Add(() => endurance());//3
        evolveFunc.Add(() => Sence5());//4
        evolveFunc.Add(() => Calamity());//5
        evolveFunc.Add(() => Breathing());//6
        evolveFunc.Add(() => Entente());//7
        evolveFunc.Add(() => SpeedRun());//8
        evolveFunc.Add(() => { });//FowardShield());//9
        evolveFunc.Add(() => { });//Charge());//10
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
    }

    public List<Evolve> Evolve()
    {
        List<Evolve> lv3lowerList = new List<Evolve>();

        for (int i = 0; i < evolveIdx.Length; i++)
        {
            if (evolveIdx[i].lv < 3)
            {
                lv3lowerList.Add(evolveIdx[i]);
            }
        }
        List<Evolve> returnValue = new List<Evolve>();

        for (int i = 0; i < 3; i++)
        {
            var x = UnityEngine.Random.Range(0, lv3lowerList.Count);
            returnValue.Add(lv3lowerList[x]);
            lv3lowerList.RemoveAt(x);
        }

        return returnValue;
    }

    public void Resiliencepeace()
    {
        var a = FindObjectOfType<PlayerData>();
        a.recovery++;
        Debug.Log("회복력 강화");
    }

    public void Beanworm()
    {
        var a = FindObjectOfType<PlayerData>();
        a.Worm++;
        Debug.Log("콩벌레");
    }

    public void GoldWorm()
    {
        var a = FindObjectOfType<PlayerData>();
        a.GoldWorm++;
        Debug.Log("돈벌레");
    }

    public void endurance()
    {
        var a = FindObjectOfType<PlayerData>();
        a.Endurance++;
        Debug.Log("지구력");
    }

    public void Sence5()
    {
        var a = FindObjectOfType<PlayerData>();
        a.Sence++;
        Debug.Log("5감각");
    }

    public void Calamity()
    {
        var a = FindObjectOfType<PlayerData>();
        a.Calamity++;
        Debug.Log("재난대처능력");
    }

    public void Breathing()
    {
        var a = FindObjectOfType<PlayerData>();
        a.Breathing++;
        Debug.Log("숨쉬기운동");
    }

    public void Entente()
    {
        var a = FindObjectOfType<PlayerData>();
        a.AddGold++;
        Debug.Log("협상");
    }

    public void SpeedRun()
    {
        var a = FindObjectOfType<PlayerData>();
        a.SpeedRun++;
        Debug.Log("질주");
    }

    public void FullCharge()
    {
        var a = FindObjectOfType<SkillSystem>();
        if (a.SkillCount < 1)
        {
            a.SkillCount++;
        }
    }

    public void MeatFall()
    {
        var a = FindObjectOfType<PlayerData>();
        var m = FindObjectOfType<CoinSpwaner>();

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < m.meatPool.Count; j++)
                {
                    if (!m.meatPool[j].activeSelf)
                    {
                        m.transform.position = a.transform.forward * i;
                        i++;
                    }
                }
            }
            m.meatPool.Add(Instantiate(m.meat, a.transform.position + a.transform.forward * i * 5f, Quaternion.identity));
        }
    }

    private bool setShield = false;

    public void DefenceUp()
    {
        var a = FindObjectOfType<PlayerData>();
        if (setShield == false)
        {
            a.animator.Play("Jumping");
            StartCoroutine(SetShield(a));
            setShield = true;
        }

        a.evolveLvData[2]++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FullCharge();
        }
    }

    private IEnumerator SetShield(PlayerData a)
    {
        //yield return new WaitForSeconds(1.5f);
        //a.shield.SetActive(true);
        GameObject b;
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            b = Instantiate(a.dummyShied, new Vector3(a.transform.position.x + 14f, a.transform.position.y + 20f, a.transform.position.z + 28f), Quaternion.identity);
        }
        else
        {
            b = Instantiate(a.dummyShied, new Vector3(a.transform.position.x - 14f, a.transform.position.y + 20f, a.transform.position.z + 28f), Quaternion.identity);
        }

        for (int i = 0; i < 45; i++)
        {
            b.transform.position = Vector3.MoveTowards(b.transform.position, a.transform.position, 45f * Time.deltaTime);
            yield return null;
        }
        a.shield.SetActive(true);
        Destroy(b.gameObject);
    }

    public void RadiationInjection()
    {
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[3]++;
    }

    public void GastrointestinalExtension()
    {
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[4]++;
    }

    public void TitaniumTooth()
    {
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[5]++;
    }

    public void QuadCore()
    {
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[7]++;
    }

    public void GoldStoker()
    {
        var a = FindObjectOfType<CoinSpwaner>().coinPool;
        var b = FindObjectOfType<PlayerData>();

        for (int i = 0; i < a.Count; i++)
        {
            a[i].transform.localScale *= 1.5f;
        }

        b.goldUpSpeed *= 2;
        b.evolveLvData[9]++;
    }

    public void FowardShield()
    {
    }

    public void Charge()
    {
    }
}