using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;

    // 아이템을 리젠하고 관리하는 장치
    [SerializeField]
    private GameObject item;

    public int keyItemCount = 20;
    public int bonuseItemCount = 10;
    public int goldItemCount = 50;
    public float minItemSpwanDistance = 50f;
    public float maxItemSpwanDistance = 60f;

    private int stage = 0;
    private bool oneMoreChance = false;
    private List<GameObject> keyItemList = new List<GameObject>();
    private List<GameObject> bonuseItemList = new List<GameObject>();
    private List<GameObject> goldItemList = new List<GameObject>();

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
        Debug.Log("a");
        Debug.Log(keyItemList.Count);
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
            a.GetComponent<Item>().type = type;
            a.SetActive(false);
            itemlist.Add(a);
        }
    }

    //배치
    private void ItemSpwan(List<GameObject> itemList, int idx = 0)
    {
        while (true)
        {
            var a = FindFarPoint(new Vector3(0, 2f, 0), minItemSpwanDistance, maxItemSpwanDistance);
            if (!SomethingOnPlace(a))
            {
                itemList[idx].transform.position = a;
                itemList[idx].SetActive(true);
                break;
            }
            else
                Debug.Log("실패");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemSpwan(keyItemList);
        }
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 dir = point - new Vector3(point.x, point.y = 50f, point.z);
        return Physics.Raycast(point, dir, 500f);
    }

    //원그리기
    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 90f, float maxDistance = 110f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }

    // 몬스터를 리젠하고 관리하는 장치

    // 함정을 리젠하고 관리하는 장치
}