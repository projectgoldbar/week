using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestStart : MonoBehaviour
{
    public void GoGame()
    {
        SceneManager.LoadScene("03.Game 3");
    }
}