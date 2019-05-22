using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public PlayerMove playerMove;
    public int skillCount = 1;
    public int maxSkillCount = 1;
    public float coolTime = 20f;

    public void SkillStart()
    {
        if (skillCount > 0)
        {
            Debug.Log("a");
            playerMove.Skill();
            skillCount -= 1;
        }
    }

    private void Update()
    {
        if (skillCount < maxSkillCount)
        {
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
            }
            else
            {
                skillCount++;
                coolTime = 20f;
            }
        }
        else
        {
            coolTime = 20f;
        }
    }
}