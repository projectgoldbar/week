using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public void OpenGame()
    {
        SceneManager.LoadScene(1);
    }
}