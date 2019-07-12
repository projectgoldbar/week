using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialC : MonoBehaviour
{
    public void GG()
    {
        Invoke("Clear", 5f);
    }


    public void Clear()
    {
        UserDataManager.Instance.userData.isTutorialClear = true;
        SceneManager.LoadScene(0);
    }
}
