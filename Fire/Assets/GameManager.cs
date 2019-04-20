using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public InGameItemContainer inGameItemContainer;
    public Vector3 goal;
    private Inventory userInventory;
    public Queue<Item_Equip> inventory;
    public int a = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        inventory = new Queue<Item_Equip>();

        userInventory = FindObjectOfType<Inventory>();
        if (a == 0)
        {
            userInventory.LoadInventory();
        }

        //if (SceneManager.sceneCountInBuildSettings == 3)
        //{
        //    inGameItemContainer = GameObject.Find("GameDataBase").GetComponent<InGameItemContainer>();
        //}
    }

    public void GameStart()
    {
        SceneManager.LoadScene("02.Loading");
    }

    public void GameEnd()
    {
        inGameItemContainer.OpenVeilAll(inventory);
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