using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

[DefaultExecutionOrder(-50)]
public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    public InGameItemContainer itemContainer;
    public NewMonsterGenerator monsterGenerator;
    public GameManager gm;

    public int stage = 0;
    private bool oneMoreChance = false;
    private Stopwatch sw = new Stopwatch();
    public Transform player;
    public bool tutorialClear = false;

    // 아이템을 리젠하고 관리하는 장치
    [SerializeField]
    private GameObject item;

    #region 아이템 젠관련 변수들

    [Header("아이템생성 부분")]
    public int keyItemCount = 20;

    public int bonuseItemCount = 10;
    public int goldItemCount = 50;

    #endregion 아이템 젠관련 변수들

    #region 임의의 2차원 원안에서 랜덤한 값을 구할때의 최소값과 최대값

    public float minItemSpwanDistance = 50f;
    public float maxItemSpwanDistance = 60f;

    #endregion 임의의 2차원 원안에서 랜덤한 값을 구할때의 최소값과 최대값

    #region 아이템리스트들

    private List<GameObject> keyItemList = new List<GameObject>();
    private List<GameObject> bonuseItemList = new List<GameObject>();
    private List<GameObject> goldItemList = new List<GameObject>();

    #endregion 아이템리스트들

    public bool genOK = false;

    //초기화
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);

        for (int i = 0; i < 3; i++)
        {
            ItemInit((ItemType)i);
        }

        if (tutorialClear)
        {
            PlayerInit();
        }
        //PointSet(300);
        if (GameObject.FindObjectOfType<GameManager>() != null)
        {
            gm = GameObject.FindObjectOfType<GameManager>();
        }
        if (tutorialClear == true)
        {
            StageUp();
        }
        //StartCoroutine(MonsterGen(delay, count));
        if (genOK == true) { monsterGenerator.GenGallery(); }
        sw.Start();
        //StageUp();
    }

    private void PlayerInit()
    {
        player.transform.position = FindEmptySpace(new Vector3(0, 0, 0), 1f, 300f);
    }

    private void ItemInit(ItemType type)
    {
        switch (type)
        {
            case ItemType.KeyItem:
                ItemSeting(keyItemList, keyItemCount, type);
                break;

            case ItemType.BonusItem:
                ItemSeting(bonuseItemList, bonuseItemCount, type);

                break;

            case ItemType.Gold:
                ItemSeting(goldItemList, goldItemCount, type);

                break;

            default:
                break;
        }
    }

    private void ItemSeting(List<GameObject> itemlist, int time, ItemType type)
    {
        for (int i = 0; i < time; i++)
        {
            var a = Instantiate(item, this.transform.position, Quaternion.identity, this.transform);
            //a.GetComponent<Item>().type = type;
            a.SetActive(false);
            itemlist.Add(a);
        }
    }

    /// <summary>
    /// 아이템을 스폰합니다.
    /// </summary>
    /// <param name="itemList">스폰할 아이템 리스트</param>
    /// <param name="count">스폰할 횟수 기본은 1회</param>
    /// <param name="idx">스폰할 키아이템의 난이도</param>
    private void ItemSpwan(List<GameObject> itemList, Vector3 point, int idx = 0, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            itemList[idx].transform.position = point;
            itemList[idx].SetActive(true);
        }
    }

    /// <summary>
    /// 포인트 위치에 뭐가 있는지 확인하는 함수
    /// </summary>
    /// <param name="point">확인하고싶은 위치</param>
    /// <returns></returns>
    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
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

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 90f, float maxDistance = 110f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }

    // 몬스터를 리젠하고 관리하는 장치부분

    private Queue<Vector3> genPointQueue = new Queue<Vector3>();

    [Header("몬스터 생성 부분")]
    public MonsterDataBase monsterdataBase;

    public GameObject[] zombielist;
    public GameObject[] specialZombies;
    public int spwanMinDistance = 70;
    public int spwanMaxDistance = 140;
    public float delay;
    public int count;

    private void PointSet(int count)
    {
        for (; genPointQueue.Count != 300;)
        {
            var p = FindEmptySpace(new Vector3(72, 1.5f, 44), spwanMinDistance, spwanMaxDistance);
            //if (!SomethingOnPlace(p))
            //{
            genPointQueue.Enqueue(p);
            //}
        }
    }

    /// <summary>
    /// 몬스터생성
    /// </summary>
    /// <param name="delay">스폰딜레이</param>
    /// <param name="count">스폰회수</param>
    /// <returns></returns>
    private IEnumerator MonsterGen(float delay, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(zombielist[0].gameObject, genPointQueue.Dequeue(), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
        yield break;
    }

    public void StageUp()
    {
        //StartCoroutine(MonsterGen(0.3f, 100));
        var a = FindEmptySpace(new Vector3(0, 1.5f, 0), 50f, 200f);
        ItemSpwan(keyItemList, a, stage);
        MarkerSystem.instance.stage = this.stage;
        MarkerSystem.instance.targetChange(a);
        for (int i = 0; i < zombielist.Length; i++)
        {
            if (zombielist[i].GetComponent<MonsterUnit>().level <= stage)
            {
                Instantiate(zombielist[i].gameObject, FindEmptySpace(player.position, 50f, 60f), Quaternion.identity);
                Instantiate(specialZombies[Random.Range(0, specialZombies.Length)].gameObject, FindEmptySpace(player.position, 50f, 60f), Quaternion.identity);
                //for (; 0 < i;)
                //{
                //    Instantiate(zombielist[i].gameObject, FindEmptySpace(player.position, 50f, 60f), Quaternion.identity);
                //}
            }
        }
    }

    private Vector3 FindEmptySpace(Vector3 pivot, float minDistance, float maxDistance)
    {
        Vector3 returnVector = new Vector3();
        while (true)
        {
            returnVector = FindFarPoint(pivot, minDistance, maxDistance);
            if (!SomethingOnPlace(returnVector))
            {
                break;
            }
        }
        return returnVector;
    }

    // 함정을 리젠하고 관리하는 장치

    //게임 종료 부분
    public GameObject gameOverUI;

    public GameObject lifeTimeText;

    public void OnGameOverPanel()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        OnLifeTimeText();
    }

    public void GameOver(bool tutorial)
    {
        gm.GameOver();
    }

    public void GameClear()
    {
        OnLifeTimeText();
        gm.GameEnd();
    }

    public void OnLifeTimeText()
    {
        sw.Stop();
        var a = sw.ElapsedMilliseconds;
        lifeTimeText.SetActive(true);
        lifeTimeText.GetComponent<Text>().text = "생존시간 :" + a / 1000;
    }
}