using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy x = null;

    private void Awake()
    {
        if (x != null && x != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            x = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}