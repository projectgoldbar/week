using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_SturnCountText : MonoBehaviour
{

    public TextMesh text;
    public TextMesh MainText;

    bool MoveGo = false;

    // Update is called once per frame
    void Update()
    {
        if (tutorialzombieTracking.ZombieSturnCounting <= 8 && !MoveGo)
        {
            text.text = $"{tutorialzombieTracking.ZombieSturnCounting} / 8";
        }
        if (tutorialzombieTracking.ZombieSturnCounting >= 8 && !MoveGo)
        {
            MainText.text = "출구문이 오픈되었습니다.  올라가서 이동하세요~!!!";
            MoveGo = true;
        }
    }
}
