using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private InGameItemContainer inGameItemContainer;

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
        if (SceneManager.sceneCountInBuildSettings == 3)
        {
            inGameItemContainer = gameObject.AddComponent<InGameItemContainer>();
        }
        DontDestroyOnLoad(gameObject);
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
}