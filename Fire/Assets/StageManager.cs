using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public List<Spawn> SpawnList = new List<Spawn>();

    public Transform[] SpawnPosition;

    public int[] overlapIndex;
    public GameObject[] OBjData;

    public List<GameObject> Monsters = new List<GameObject>();

    private WaitForSeconds ShowTimer;

    public Light LightColor;

    private void Start()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        ShowTimer = new WaitForSeconds(0.5f);
    }

    public void StageSetting(int StageLv)
    {
        //해당 스테이지에 실행되야할것
        StartCoroutine(StageMonster(StageLv));
    }

    #region 몬스터

    public Spawn GetSpawn(int StageLv)
    {
        var Data = SpawnList.Find(x => x.StageLv == StageLv);
        return Data;
    }

    public IEnumerator StageMonster(int StageLv)
    {
        var StageData = GetSpawn(StageLv);

        //yield return StartCoroutine(DataReset());
        yield return StartCoroutine(StageMonsterCreateNHide(StageData));
        StartCoroutine(ShowStageMonster());
    }

    public IEnumerator DataReset()
    {
        if (Monsters.Count > 0)
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                Destroy(Monsters[i].gameObject);
                yield return null;
            }
        }
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        Monsters.Clear();
        OBjData = new GameObject[0];
    }

    private IEnumerator ShowStageMonster()
    {
        for (int j = 0; j < Monsters.Count; j++)
        {
            if (!Monsters[j].activeSelf)
            { Monsters[j].SetActive(true); }
            yield return ShowTimer;
        }
    }

    private IEnumerator StageMonsterCreateNHide(Spawn StageData)
    {
        SpawnData SpawnCount = new SpawnData();
        int Count = 0;
        LightColor.color = StageData.PlayerPointLight;
        for (int k = 0; k < StageData.spawnData.data.Length; k++)
        {
            SpawnCount = StageData.spawnData.data[k];

            Count += SpawnCount.SpawnCount;

            //2 ,3
            for (int l = 0; l < SpawnCount.SpawnCount; l++)
            {
                Array.Resize(ref OBjData, OBjData.Length + 1);
                OBjData[OBjData.Length - 1] = SpawnCount.SpawnOBJ;
            }
        }

        for (int j = 0; j < Count; j++)
        {
            int randomIndex = overlapIndex[j % overlapIndex.Length];

            var CreateStageData = GameObject.Instantiate(OBjData[j], SpawnPosition[randomIndex]);
            CreateStageData.SetActive(false);
            Monsters.Add(CreateStageData);
            yield return ShowTimer;
        }
    }

    /// <summary>
    /// 랜덤인덱스 뽑기 (겹침 있슴.)
    /// </summary>
    /// <returns></returns>
    public int RandomIndex()
    {
        int SpawnIndex = UnityEngine.Random.Range(0, SpawnPosition.Length);

        return SpawnIndex;
    }

    /// <summary>
    /// 랜덤인덱스 뽑기
    /// https://citynetc.tistory.com/194
    /// 그대로 가져옴
    /// </summary>
    /// <returns></returns>
    public int[] OverlapRandomIndex(int range)
    {
        int[] randArray = new int[range];
        bool isSame;

        for (int i = 0; i < range; i++)
        {
            while (true)
            {
                randArray[i] = UnityEngine.Random.Range(0, SpawnPosition.Length);
                isSame = false;
                for (int j = 0; j < i; j++)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArray;
    }

    #endregion 몬스터

    #region 테스트

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StageSetting(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StageSetting(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StageSetting(2);
        }
    }

    #endregion 테스트
}

[System.Serializable]
public class SpawnData
{
    public GameObject SpawnOBJ;         //스폰될 오브젝트
    public int SpawnCount;              //생성될 갯수
}

[System.Serializable]
public class SpawnRange
{
    public SpawnData[] data;
}

[System.Serializable]
public class Spawn
{
    [Header("스테이지 검색용")]
    public int StageLv;

    public string StageName;

    public Color PlayerPointLight;

    [Header("스폰데이터")]
    public SpawnRange spawnData;

    private bool dataShowHide;

    public bool DataShowHide
    {
        get { return dataShowHide; }
        set
        {
            dataShowHide = value;
            if (dataShowHide)
            {
                for (int i = 0; i < spawnData.data.Length; i++)
                {
                    var SpawnCount = spawnData.data[i].SpawnCount;

                    for (int j = 0; j < SpawnCount; j++)
                    {
                        spawnData.data[j].SpawnOBJ.SetActive(true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < spawnData.data.Length; i++)
                {
                    var SpawnCount = spawnData.data[i].SpawnCount;

                    for (int j = 0; j < SpawnCount; j++)
                    {
                        spawnData.data[j].SpawnOBJ.SetActive(false);
                    }
                }
            }
        }
    }
}