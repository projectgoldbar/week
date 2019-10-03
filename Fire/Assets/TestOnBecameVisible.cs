using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnBecameVisible : MonoBehaviour
{
   

    void OnBecameVisible()
    {
        Debug.Log("화면에 나타남");
    }


    void OnBecameInvisible()
    {
        Debug.Log("화면에서 사라짐");
    }
}
