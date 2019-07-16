using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject[] RefBoxs;
    public GameObject zombie;
    public GameObject dashZombie;
    public GameObject spitZombie;
    public TestCoinSpwan testCoinSpwan;
    public TargetPointer tarGetPointer;

    public List<GameObject> zombiePool;
    public List<GameObject> dashZombiePool;
    public List<GameObject> etcPool;

    public Manager manager;
    public GameObject pivot; //박스떨구기위해 있는 피벗
    public Text lvTextUI;
    public List<StageS> stageList = new List<StageS>();
    public UITweenEffectManager UITweenEffectManager;

    [HideInInspector]
    public PlayerData playerData;

    [HideInInspector]
    public ParticlePool particlePool;

    public CoinPool coinPool;
    public Transform[] SpawnPosition;

    public int[] overlapIndex;

    public GameObject[] OBjData;

    private WaitForSeconds ShowTimer;

    public Light lightColor;

    public int currentStageLV = 0;
    public int maxStageCount = 0;
    private int randomValue;

    //

    private void Awake()
    {
        overlapIndex = OverlapRandomIndex(SpawnPosition.Length);
        ShowTimer = new WaitForSeconds(0.5f);
        playerData = FindObjectOfType<PlayerData>();
        particlePool = FindObjectOfType<ParticlePool>();
        maxStageCount = stageList.Count;
        if (!playerData.isTutirial)
        {
            ZombiePoolSet();
        }

        StageSetting();
    }

    #region 좀비풀

    private void ZombiePoolSet()
    {
        for (int i = 0; i < 25; i++)
        {
            var y = Instantiate(dashZombie, transform.position, Quaternion.identity, transform);

            y.SetActive(false);
            dashZombiePool.Add(y);
        }
        for (int i = 0; i < 10; i++)
        {
            var z = Instantiate(spitZombie, transform.position, Quaternion.identity, transform);
            z.SetActive(false);
            etcPool.Add(z);
        }
    }

    private GameObject GetZombie(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        var a = Instantiate(pool[0], transform.position, Quaternion.identity, transform);
        a.SetActive(false);
        pool.Add(a);
        return a;
    }

    #endregion 좀비풀

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
        if (playerData.isTutirial)
        {
            go.AddComponent<TutorialClear>();
        }
        go.transform.position = SpwanTransform;
        yield return null;
        yield break;
    }

    private IEnumerator StageChangeLighting(Color Current, Color Next, int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0)
            { lightColor.color = Current; }
            else
            { lightColor.color = Next; }

            yield return null;
        }
    }

    public void StageSetting()
    {
        if (currentStageLV >= maxStageCount)
        {
            InfinityMode();
            return;
        }
        //현제스테이지
        var stageData = stageList[currentStageLV];
        //해당 스테이지에 실행되야할것
        //스테이지 전환효과
        lightColor.color = stageData.PlayerPointLight;
        //Color CurrentColor = lightColor.color;
        //Color NextColor = stageData.PlayerPointLight;

        //StartCoroutine(StageChangeLighting(CurrentColor, NextColor, 2));

        ///////////////////////////////////////////////
        //몬스터생성
        if (!playerData.isTutirial)
        {
            StartCoroutine(MonsterCreate(stageList[currentStageLV]));
        };
        //몬스터강화
        if (!playerData.isTutirial)
        {
            MonsterUpgrade(stageData);
        }

        //박스생성
        Vector3 boxPosition = FindPoint();
        boxPosition.y += 9.5f;
        StartCoroutine(CrearRewardBox(currentStageLV, boxPosition));
        //레벨업
        if (currentStageLV > 0)
        {
            LvUp();
        }
        //유저 박스화살표 변경
        //boxIndirection.target = boxPosition;
        tarGetPointer.targetPosition = boxPosition;
        UITweenEffectManager.stageOpenPanel.gameObject.SetActive(true);
        UITweenEffectManager.stageOpenPanel.OpenPanel("Lv  " + currentStageLV.ToString());
        //지뢰가 생성되는 개수
        //int mineCount = stageData.spawnData.data[2].SpawnCount;
        //지뢰개수만큼 지속적으로 지뢰 생성되는함수 구현하기
        //코인개수 만큼 뿌리기 구현
        //UI LV올리기
        if (!playerData.isTutirial)
        {
            lvTextUI.text = "LV" + currentStageLV.ToString();
        }
        playerData.GetComponent<PlayerMove>().maxSpeed += 0.2f;
    }

    #region 30스테이지 이상일 때

    private void InfinityMode()
    {
        var stageData = stageList[maxStageCount];

        lightColor.color = stageList[Random.Range(0, maxStageCount)].PlayerPointLight;

        MonsterUpgrade(stageData);
        Vector3 boxPosition = FindPoint();
        boxPosition.y += 9.5f;
        StartCoroutine(CrearRewardBox(currentStageLV, boxPosition));
        if (currentStageLV > 0)
        {
            LvUp();
        }
        tarGetPointer.targetPosition = boxPosition;
        UITweenEffectManager.stageOpenPanel.gameObject.SetActive(true);
        UITweenEffectManager.stageOpenPanel.OpenPanel("Infinity  " + currentStageLV.ToString());
        lvTextUI.text = "LV" + currentStageLV.ToString();
        playerData.GetComponent<PlayerMove>().maxSpeed += 0.2f;
    }

    #endregion 30스테이지 이상일 때

    #region 스테이지 레벨업 시퀀스

    public void LvUp()
    {
        var targets = Physics.OverlapSphere(playerData.transform.position, 35f, LayerMask.GetMask("Monster"));
        if (!playerData.isTutirial)
        {
            testCoinSpwan.StopSpwan();
            testCoinSpwan.StopMeatMethod();
        }
        StartCoroutine(ChangeAsh(targets));
    }

    public void TutorialLvUP()
    {
        var targets = Physics.OverlapSphere(playerData.transform.position, 45f, LayerMask.GetMask("Monster"));
    }

    private IEnumerator ChangeAsh(Collider[] targets)
    {
        WaitForSeconds seconds = new WaitForSeconds(0.1f);
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.black;
            yield return null;
        }
        for (int i = 0; i < targets.Length; i++)
        {
            var dust = particlePool.GetParticle(particlePool.zombieDustParticle);
            dust.transform.position = targets[i].transform.position;
            dust.transform.rotation = Quaternion.LookRotation(Vector3.up);
            dust.SetActive(true);
            //targets[i].gameObject.SetActive(false);
            targets[i].transform.position = SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)].position;
            targets[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
            yield return null;
        }
        for (int i = 0; i < 15; i++)
        {
            yield return seconds;
        }

        if (!playerData.isTutirial)
        {
            manager.Evolution();
            var coinPools = coinPool.coinPool;

            Queue<GameObject> activeCoins = new Queue<GameObject>();

            for (int i = 0; i < coinPools.Count; i++)
            {
                if (coinPools[i].activeSelf == true)
                {
                    coinPools[i].GetComponent<Coin>().coinRotate = false;
                    activeCoins.Enqueue(coinPools[i]);
                }
            }
            for (; 1 < activeCoins.Count;)
            {
                var x = activeCoins.Dequeue();
                var y = activeCoins.Dequeue();
                for (; y.activeSelf == true;)
                {
                    x.transform.position = Vector3.Lerp(x.transform.position, playerData.transform.position, 20f * Time.deltaTime);
                    y.transform.position = Vector3.Lerp(y.transform.position, playerData.transform.position, 20f * Time.deltaTime);
                    yield return null;
                }
            }

            testCoinSpwan.SpwanGold();
            testCoinSpwan.SpwanMeatMethod();
        }
    }

    #endregion 스테이지 레벨업 시퀀스

    #region 몬스터

    //public StageS GetSpawn(int StageLv)
    //{
    //    var Data = stageList.Find(x => x.StageLv == StageLv);
    //    return Data;
    //}

    private void MonsterUpgrade(StageS ss)
    {
        var StageData = ss;

        //해당 스테이지에서 몬스터의 추가속도를 더해줌
        //해당 스테이지에서 몬스터의 추가 공격력을 더해줌

        for (int i = 0; i < zombiePool.Count; i++)
        {
            zombiePool[i].GetComponent<ZombieState.ZombiesComponent>().damage += StageData.spawnData.AddDamage;
            etcPool[i].GetComponent<ZombieState.Zombie_AttackRun>().accelSpeed += StageData.spawnData.AddSpeed;
        }
        for (int i = 0; i < dashZombiePool.Count; i++)
        {
            dashZombiePool[i].GetComponent<ZombieState.ZombiesComponent>().damage += StageData.spawnData.AddDamage;
            dashZombiePool[i].GetComponent<ZombieState.ZomBie_Attack>().originSpeed += StageData.spawnData.rushSpeed;
        }
        for (int i = 0; i < etcPool.Count; i++)
        {
            etcPool[i].GetComponent<ZombieState.ZombiesComponent>().damage += StageData.spawnData.AddDamage;
            etcPool[i].GetComponent<ZombieState.ZombieRunBite>().accelSpeed += StageData.spawnData.AddSpeed;
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
            var z = GetZombie(zombiePool);
            z.transform.position = SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)].position;
            z.SetActive(true);
            yield return ShowTimer;
        }
        for (int i = 0; i < StageData.spawnData.dashZombieCount; i++)
        {
            var z = GetZombie(dashZombiePool);
            z.transform.position = SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)].position;
            z.SetActive(true);
            yield return ShowTimer;
        }
        for (int i = 0; i < StageData.spawnData.spitZombieCount; i++)
        {
            var z = GetZombie(etcPool);
            z.transform.position = SpawnPosition[UnityEngine.Random.Range(0, SpawnPosition.Length)].position;
            z.SetActive(true);
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
        if (manager.score > 4000f + (currentStageLV * 4000f))
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
    public float rushSpeed;
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