using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public GameObject[] RefBoxs;
    public GameObject zombie;
    public GameObject dashZombie;
    public GameObject spitZombie;
    public ArrowMove boxIndirection;
    public Manager manager;
    public GameObject pivot; //박스떨구기위해 있는 피벗
    public List<StageS> stageList = new List<StageS>();

    public Transform[] SpawnPosition;

    public int[] overlapIndex;

    public GameObject[] OBjData;

    private WaitForSeconds ShowTimer;

    public Light lightColor;

    //맵안에 생성될 모든 좀비
    public List<ZombieState.ZombiesComponent> allZombies = new List<ZombieState.ZombiesComponent>();

    public int currentStageLV = 0;

    private int randomValue;

    //
    public int GetBoxRandom()
    {
        //var StageData = GetSpawn(StageLv);
        randomValue = UnityEngine.Random.Range(0, 101);
        int bronzePerCent = stageList[currentStageLV].spawnData.percent_copper;
        int silverPerCent = bronzePerCent + stageList[currentStageLV].spawnData.percent_silver;
        int goldPerCent = silverPerCent + stageList[currentStageLV].spawnData.percent_gold;
        //동박스
        if (bronzePerCent > randomValue)
            return 0;
        //은박스
        else if (silverPerCent > randomValue)
            return 1;
        //금박스
        else
            return 2;
    }

    private IEnumerator CrearRewardBox(int StageLv, Vector3 SpwanTransform)
    {
        var go = GameObject.Instantiate(RefBoxs[GetBoxRandom()]);
        go.transform.position = SpwanTransform;
        yield return null;
    }

    public void StageSetting()
    {
        //현제스테이지
        var stageData = stageList[currentStageLV];
        //해당 스테이지에 실행되야할것

        //스테이지 전환효과
        lightColor.color = stageData.PlayerPointLight;
        //몬스터생성
        StartCoroutine(MonsterCreate(stageList[currentStageLV]));
        //몬스터강화
        MonsterUpgrade();

        //박스생성
        Vector3 boxPosition = FindPoint();
        StartCoroutine(CrearRewardBox(currentStageLV, boxPosition));
        //레벨업
        if (currentStageLV > 0)
        {
            manager.Evolution();
        }
        //유저 박스화살표 변경
        boxIndirection.target = boxPosition;
        //지뢰가 생성되는 개수
        //int mineCount = stageData.spawnData.data[2].SpawnCount;
        //지뢰개수만큼 지속적으로 지뢰 생성되는함수 구현하기

        //코인개수 만큼 뿌리기 구현

        Debug.Log("aaaaaaaaaaa" + currentStageLV);
    }

    private void Start()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        ShowTimer = new WaitForSeconds(0.5f);
        StageSetting();
    }

    #region 몬스터

    //public StageS GetSpawn(int StageLv)
    //{
    //    var Data = stageList.Find(x => x.StageLv == StageLv);
    //    return Data;
    //}

    private void MonsterUpgrade()
    {
        var StageData = stageList[currentStageLV];

        //해당 스테이지에서 몬스터의 추가속도를 더해줌
        //해당 스테이지에서 몬스터의 추가 공격력을 더해줌

        foreach (var item in allZombies)
        {
            item.agent.speed += StageData.spawnData.AddSpeed;
            item.damage += StageData.spawnData.AddDamage;
        }
    }

    public IEnumerator DataReset()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        OBjData = new GameObject[0];
        yield return null;
    }

    private IEnumerator MonsterCreate(StageS StageData)
    {
        //SpawnData SpawnCount = new SpawnData();
        //int Count = 0;

        for (int i = 0; i < StageData.spawnData.zombieCount; i++)
        {
            var z = Instantiate(zombie, SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)], transform);
            allZombies.Add(z.GetComponent<ZombieState.ZombiesComponent>());
            yield return ShowTimer;
        }
        for (int i = 0; i < StageData.spawnData.dashZombieCount; i++)
        {
            var z = Instantiate(dashZombie, SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)], transform);
            allZombies.Add(z.GetComponent<ZombieState.ZombiesComponent>());
            yield return ShowTimer;
        }
        for (int i = 0; i < StageData.spawnData.spitZombieCount; i++)
        {
            var z = Instantiate(spitZombie, SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)], transform);
            allZombies.Add(z.GetComponent<ZombieState.ZombiesComponent>());
            yield return ShowTimer;
        }
        //for (int k = 0; k < StageData.spawnData.data.Length; k++)
        //{
        //    SpawnCount = StageData.spawnData.data[k];

        //    Count += SpawnCount.SpawnCount;

        //    //2 ,3
        //    for (int l = 0; l < SpawnCount.SpawnCount; l++)
        //    {
        //        Array.Resize(ref OBjData, OBjData.Length + 1);
        //        OBjData[OBjData.Length - 1] = SpawnCount.SpawnOBJ;
        //    }
        //}

        //for (int j = 0; j < Count; j++)
        //{
        //    int randomIndex = overlapIndex[j % overlapIndex.Length];

        //    var CreateStageData = GameObject.Instantiate(OBjData[j], SpawnPosition[randomIndex]);
        //    allZombies.Add(CreateStageData.GetComponent<ZombieState.ZombiesComponent>());
        //    yield return ShowTimer;
        //}
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
        if (manager.score > stageList[currentStageLV].spawnData.ClearScore)
        {
            currentStageLV++;
            StageSetting();
        }
    }

    #endregion 테스트

    public Vector3 FindPoint()
    {
        var bounds = pivot.GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max;

        for (int i = 0; i < 50; i++)
        {
            var x = UnityEngine.Random.Range(min.x, max.x);
            var z = UnityEngine.Random.Range(min.z, max.z);
            Vector3 targetVector = new Vector3(x, 1.7f, z);
            if (!SomethingOnPlace(targetVector))
            {
                return targetVector;
            }
        }
        return new Vector3(1000f, 1.7f, 1000f);
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 피벗기준으로 rotation방향으로 distance만큼 떨어진 위치를 반환
    /// </summary>
    /// <param name="pivot"></param>
    /// <param name="rotation"></param>
    private Vector3 PivotPointSet(Vector3 pivot, Vector3 origin, Direction direction, float distance)
    {
        switch (direction)
        {
            case Direction.Left:
                pivot = origin;
                pivot.x -= distance;
                break;

            case Direction.Right:
                pivot = origin;
                pivot.x += distance;
                break;

            case Direction.Foward:
                pivot = origin;
                pivot.z += distance;

                break;

            case Direction.Back:
                pivot = origin;
                pivot.z -= distance;

                break;

            default:
                break;
        }
        return pivot;
    }
}

//[System.Serializable]
//public class SpwanMonsterCount
//{
//}

[System.Serializable]
public class SpawnRange
{
    public int ClearScore;              //스테이지 클리어점수
    public float AddSpeed;              //좀비들의 추가되는 이동속도
    public int AddDamage;               //좀비들의 추가되는 공격력

    public int percent_copper;          //동박스 확률   0
    public int percent_silver;          //은박스 확률   1
    public int percent_gold;            //금박스 확률   2

    public int zombieCount;             //생성될 갯수
    public int dashZombieCount;
    public int spitZombieCount;
    public int mineCount;
}

[System.Serializable]
public class StageS
{
    [Header("스테이지 검색용")]
    public int StageLv;

    public string StageName;

    public Color PlayerPointLight;

    [Header("스폰데이터")]
    public SpawnRange spawnData;

    //private bool dataShowHide;

    //public bool DataShowHide
    //{
    //    get { return dataShowHide; }
    //    set
    //    {
    //        dataShowHide = value;
    //        if (dataShowHide)
    //        {
    //            for (int i = 0; i < spawnData.data.Length; i++)
    //            {
    //                var SpawnCount = spawnData.data[i].SpawnCount;

    //                for (int j = 0; j < SpawnCount; j++)
    //                {
    //                    spawnData.data[j].SpawnOBJ.SetActive(true);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < spawnData.data.Length; i++)
    //            {
    //                var SpawnCount = spawnData.data[i].SpawnCount;

    //                for (int j = 0; j < SpawnCount; j++)
    //                {
    //                    spawnData.data[j].SpawnOBJ.SetActive(false);
    //                }
    //            }
    //        }
    //    }
    //}
}