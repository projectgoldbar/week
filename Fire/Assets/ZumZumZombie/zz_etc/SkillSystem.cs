using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public PlayerMove playerMove;
    public PlayerData playerData;
    public int skillCount = 1;
    public int maxSkillCount = 1;
    public float originCoolTime = 20f;
    public float coolTime = 20f;
    public Image progressBar;
    public Text skillCounttext;

    public int SkillCount
    {
        get
        {
            return skillCount;
        }
        set
        {
            skillCount = value;
            skillCounttext.text = skillCount.ToString();
        }
    }

    private void Awake()
    {
        progressBar = GetComponent<Image>();
        coolTime = originCoolTime;
        skillCount = maxSkillCount;
        skillCounttext.text = skillCount.ToString();
    }


    private void Start()
    {
        playerData = playerMove.GetComponent<PlayerData>();
        
    }

    public void SkillStart()
    {
        if (SkillCount != 0)
        {
            playerMove.Skill();

            if (!coolTimeRunning)
                coolTime = 0;

            SkillCount -= 1;
        }
    }

    private bool coolTimeRunning = false;

    private void Update()
    {
        if (SkillCount < playerData.skillCountLv)
        {
            if (coolTime >= originCoolTime)
            {
                SkillCount++;

                if (SkillCount < playerData.skillCountLv)
                {
                    coolTime = 0;
                }
            }
            else
            {
                coolTime += Time.deltaTime;
                coolTimeRunning = true;
            }
        }
        else
        {
            coolTime = originCoolTime;
            coolTimeRunning = false;
        }
        progressBar.fillAmount = coolTime * 0.05f;
    }
}