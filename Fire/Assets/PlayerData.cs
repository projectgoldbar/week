using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float maxhp = 50f;
    public float hp = 50f;
    public float consumeLv = 0;
    public float skillLv = 0;
    public float skillCountLv = 0;

    private void Awake()
    {
        hp = maxhp;
        //FindObjectOfType<hpSlider>().playerData = this;
        FindObjectOfType<SkillSystem>().playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        hp = hp - 3.3f * Time.deltaTime;
    }
}