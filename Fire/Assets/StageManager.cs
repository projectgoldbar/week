using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public GameObject[] RefBoxs;

    public List<StageSpawn> SpawnList = new List<StageSpawn>();

    public Transform[] SpawnPosition;

    public int[] overlapIndex;

    public GameObject[] OBjData;

    private WaitForSeconds ShowTimer;

    public Light LightColor;

    //맵안에 생성될 모든 좀비
    public List<ZombieState.ZombiesComponent> AllZombies = new List<ZombieState.ZombiesComponent>();

    public int currentStageLV = 0;

    private int Rndpercent;


    //
    public int RndPer(int StageLv)
    {
        var StageData = GetSpawn(StageLv);
        Rndpercent = UnityEngine.Random.Range(0, 101);

        if (StageData.spawnData.percent_copper <= Rndpercent)
            return 0;
        else if (StageData.spawnData.percent_copper > Rndpercent &&
                 StageData.spawnData.percent_silver <= Rndpercent)
            return 1;
        else
            return 2;
    }

    private IEnumerator CrearRewardBox(int StageLv, Vector3 SpwanTransform)
    {
        var go = GameObject.Instantiate(RefBoxs[RndPer(StageLv)]);

        go.transform.position = SpwanTransform;
        yield return null;
    }

    public void StageSetting(int StageLv)
    {
        
        //현제스테이지
        var StageData = SpawnList[StageLv];
        //해당 스테이지에 실행되야할것
        

        //스테이지 전환효과
        
        //몬스터생성-몬스터 속도증가
        StartCoroutine(StageMonster(StageLv));

        //박스생성

        Vector3 boxPosition = new Vector3(0, 0, 0);
        StartCoroutine(CrearRewardBox(StageLv, boxPosition));

        //유저 박스화살표 변경

        //지뢰가 생성되는 개수
        int mineCount = StageData.spawnData.data[2].SpawnCount;
        //지뢰개수만큼 지속적으로 지뢰 생성되는함수 구현하기

        //코인개수 만큼 뿌리기 구현

        Debug.Log("aaaaaaaaaaa"+StageLv);
    }

    private void Start()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        ShowTimer = new WaitForSeconds(0.5f);
    }


    #region 몬스터

    public StageSpawn GetSpawn(int StageLv)
    {
        var Data = SpawnList.Find(x => x.StageLv == StageLv);
        return Data;
    }

    public IEnumerator StageMonster(int StageLv)
    {
        var StageData = SpawnList[currentStageLV];


        //해당 스테이지에서 몬스터의 추가속도를 더해줌
        //해당 스테이지에서 몬스터의 추가 공격력을 더해줌

        foreach (var item in AllZombies)
        {
            item.agent.speed += StageData.spawnData.AddSpeed;
            item.damage += StageData.spawnData.AddDamage;
        }

        yield return StartCoroutine(DataReset());
        
        yield return StartCoroutine(StageMonsterCreateNHide(StageData));
    }

    public IEnumerator DataReset()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        OBjData = new GameObject[0];
        yield return null;
    }

   

    private IEnumerator StageMonsterCreateNHide(StageSpawn StageData)
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
            AllZombies.Add(CreateStageData.GetComponent<ZombieState.ZombiesComponent>());
            yield return ShowTimer;
        }
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

    public int ClearScore;              //스테이지 클리어점수
    public float AddSpeed;              //좀비들의 추가되는 이동속도
    public int AddDamage;               //좀비들의 추가되는 공격력

    public int percent_copper;          //동박스 확률   0
    public int percent_silver;          //은박스 확률   1
    public int percent_gold;            //금박스 확률   2
}

[System.Serializable]
public class StageSpawn
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