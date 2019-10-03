using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestStart : MonoBehaviour
{
    public static TestStart instance;

    public float a = 7;

    private void Awake()
    {
        instance = this;
    }
}