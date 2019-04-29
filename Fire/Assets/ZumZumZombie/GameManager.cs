using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-300)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public InGameItemContainer inGameItemContainer;
    public Vector3 goal;
    private Inventory userInventory;

    public static Queue<Item_Equip> inventory = new Queue<Item_Equip>();
    public int a = 0;
    public int playerHp = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //inventory = new Queue<Item_Equip>();

        userInventory = FindObjectOfType<Inventory>();
        if (a == 0)
        {
            userInventory.LoadInventory();
            if (inventory != null)
            {
                PutItemOnInventory(userInventory.itemList);
            }
            userInventory.RefreshStatus();
            userInventory.SaveInventory();
        }
        //if (SceneManager.sceneCountInBuildSettings == 3)
        //{
        //    inGameItemContainer = GameObject.Find("GameDataBase").GetComponent<InGameItemContainer>();
        //}
    }

    public void GameStart()
    {
        userInventory.SaveInventory();
        SceneManager.LoadScene("02.Loading");
    }

    public void GameEnd()
    {
        inGameItemContainer.OpenVeilAll(inventory);
        Debug.Log("내인벤토리에 잇는 아이템의 수" + inventory.Count);
        SceneManager.LoadScene("01.Intro");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("01.Intro");
    }

    public void PutItemOnInventory(List<Item_Equip> list)
    {
        Debug.Log("넣기");
        for (int i = 0; i < inventory.Count; i++)
        {
            list.Add(inventory.Dequeue());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEnd();
        }
        //Debug.Log(Inventory[1]);
        //if (Vector3.Distance(Utility.Instance.playerTr.position, goal) < 1f)
    }
}