﻿using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public PlayerMove playerMove;
    public int skillCount = 1;
    public int maxSkillCount = 1;
    public float originCoolTime = 20f;
    public float coolTime = 20f;
    public Image progressBar;

    private void Awake()
    {
        progressBar = GetComponent<Image>();
        coolTime = originCoolTime;
    }

    public void SkillStart()
    {
        if (skillCount > 0)
        {
            Debug.Log("a");
            coolTime = 0;
            playerMove.Skill();
            skillCount -= 1;
        }
    }

    private void Update()
    {
        if (skillCount < maxSkillCount)
        {
            if (coolTime >= originCoolTime)
            {
                skillCount++;
            }
            else
            {
                coolTime += Time.deltaTime;
            }
        }
        else
        {
            coolTime = originCoolTime;
        }
        progressBar.fillAmount = coolTime * 0.05f;
    }
}