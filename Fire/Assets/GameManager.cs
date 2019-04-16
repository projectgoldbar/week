using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public InGameItemContainer inGameItemContainer;

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
        //if (SceneManager.sceneCountInBuildSettings == 3)
        //{
        //    inGameItemContainer = GameObject.Find("GameDataBase").GetComponent<InGameItemContainer>();
        //}
    }

    public List<Item> Inventory;

    public void GameStart()
    {
        SceneManager.LoadScene("02.Loading");
    }

    public void GameEnd()
    {
        inGameItemContainer.OpenVeilAll(Inventory);
        SceneManager.LoadScene("01.Intro");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEnd();
        }
    }
}