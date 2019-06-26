using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorWin : EditorWindow
{

    public string StopText = "";
    public GameObject card1, card2, card3;

    public bool StopFlag = false;
    [MenuItem("TestEditor/EditorWindow")]
    public static void OPEN()
    {
        //Editor 창 하나를 띄우는데 창의 이름을 넣어줄수 있다.
        //GetWindow<EditorWin>()                        창을 띄움     
        //titleContent = new GUIContent("GameTitle");   창의 TITLE명을 수정 
        GetWindow<EditorWin>().titleContent = new GUIContent("GameTitle");
    }


    public void OnGUI()
    {
        GUILayout.Label("여기는 뭐가 쓰이나요?");

        //오브젝트를 넣을수있는 칸이 생긴다 . 
        card1 = (GameObject)EditorGUILayout.ObjectField(card1,typeof(GameObject),true);
        card2 = (GameObject)EditorGUILayout.ObjectField(card2,typeof(GameObject),true);
        card3 = (GameObject)EditorGUILayout.ObjectField(card3,typeof(GameObject),true);


        //버튼이 생겨서 버튼을 누르면 if문으로 들어옴.
        if (GUILayout.Button(StopText))
        {
            //  ShuffleCard(card1,card2);

            StopFlag = !StopFlag;

            if (StopFlag)
            {
                StopText = "스탑";
            }
            else
            {
                StopText = "재생";
            }

        }
    }

    float timer = 0;
    private void Update()
    {
        if (StopFlag)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                ShuffleCard(card1, card2);
                timer = 0;
            }
        }
    }


    public void ShuffleCard(GameObject go1 , GameObject go2)
    {
        int rnd = Random.Range(0, 2);

        var pos = go1.transform.localPosition;
        go1.transform.localPosition = go2.transform.localPosition;
        go2.transform.localPosition = pos;
    }
     
}
