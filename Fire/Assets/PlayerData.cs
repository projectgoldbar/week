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
    public float maxEp = 10f;
    public float epLv = 0;
    public float ep = 0f;
    public int gold = 0;
    public int df = 0;
    private bool isGameOver = false;
    private Manager manager;

    public float Ep
    {
        get
        {
            return ep;
        }
        set
        {
            ep += value;
            if (ep >= maxEp)
            {
                epLv++;
                ep = ep - maxEp;
                maxEp = maxEp + epLv;
                manager.Evolution();
            }
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold += value;
            manager.goldUi.text = gold.ToString();
        }
    }

    private void Awake()
    {
        hp = maxhp;
        //FindObjectOfType<hpSlider>().playerData = this;
        FindObjectOfType<SkillSystem>().playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (hp >= 0)
            {
                hp = hp - 3.3f * Time.deltaTime;
            }
            else
            {
                FindObjectOfType<Manager>().GameOver();
                isGameOver = true;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            hp -= (other.GetComponent<ZombieState.ZombiesComponent>().damage - df);
        }
    }
}