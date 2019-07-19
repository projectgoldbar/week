using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_SturnCountText : MonoBehaviour
{

    public TextMesh textMesh;
    public TextMesh MainTextMesh;

    bool MoveGo = false;

    // Update is called once per frame
    void Update()
    {
        if (tutorialzombieTracking.ZombieSturnCounting <= 8 && !MoveGo)
        {
            textMesh.text = $"{tutorialzombieTracking.ZombieSturnCounting}/2";
        }
        if (tutorialzombieTracking.ZombieSturnCounting >= 8 && !MoveGo)
        {
            MainTextMesh.text = "좌측에 문이 오픈되었습니다. 이동하세요~!!!";
            MoveGo = true;
        }
    }
}
