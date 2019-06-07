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
}

public class EvolveSystem : MonoBehaviour
{
    public Evolve[] evolveIdx;

    public List<Action> evolveFunc = new List<Action>();

    private void Awake()
    {
        evolveFunc.Add(() => SecondsHeart());
        evolveFunc.Add(() => FullCharge());
        evolveFunc.Add(() => MeatFall());
        evolveFunc.Add(() => DefenceUp());
        evolveFunc.Add(() => RadiationInjection());
        evolveFunc.Add(() => GastrointestinalExtension());
        evolveFunc.Add(() => TitaniumTooth());
        evolveFunc.Add(() => MagnetTail());
        evolveFunc.Add(() => QuadCore());
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
        evolveFunc.Add(() => { });
        evolveFunc.Add(() => { });
    }

    public List<Evolve> Evolve()
    {
        List<bool> boolIdx = new List<bool>();
        for (int i = 0; i < evolveIdx.Length; i++)
        {
            boolIdx.Add(true);
        }
        List<Evolve> returnValue = new List<Evolve>();

        for (int j = 0; j < 3; j++)
        {
            var x = UnityEngine.Random.Range(0, evolveIdx.Length);
            if (boolIdx[x] == true)
            {
                boolIdx[x] = false;
                returnValue.Add(evolveIdx[x]);
            }
            else
            {
                j--;
            }
        }
        return returnValue;
    }

    public void SecondsHeart()
    {
        var a = FindObjectOfType<PlayerData>();
        a.live++;
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
        a.evolveLvData[4]++;
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
        Debug.Log("1");
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[5]++;
    }

    public void GastrointestinalExtension()
    {
        Debug.Log("2");

        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[6]++;
    }

    public void TitaniumTooth()
    {
        Debug.Log("3");

        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[7]++;
    }

    public void MagnetTail()
    {
        Debug.Log("4");

        var a = FindObjectOfType<PlayerData>();
        a.magnet.SetActive(true);
        a.MagnetLV = 1;
    }

    public void QuadCore()
    {
        var a = FindObjectOfType<PlayerData>();
        a.evolveLvData[8]++;
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
    }
}