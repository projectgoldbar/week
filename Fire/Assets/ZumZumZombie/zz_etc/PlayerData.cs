using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public GameObject dummyShied;
    public ParticleSystem boostParticle;
    public bool isTest;
    public Text hpText;

    private bool smite = true;

    public float maxhp = 50f;
    public float hp = 50f;
    public float hpDownSpeed = 5f;
    public float score = 0;

    /// <summary>
    /// 스킬지속시간관련
    /// </summary>
    public float skillLv = 0;

    /// <summary>
    /// 스킬최대횟수관련
    /// </summary>
    public float skillCountLv = 1;

    public float maxEp = 10f;
    public float epLv = 0;
    public float ep = 0f;
    public float hpUpSpeed = 5f;
    public float epUpSpeed = 3f;
    public float goldUpSpeed = 5;
    public float gold = 0;
    public int df = 0;
    public int live = 0;
    public int randomBoxCount = 0;
    public int clearCount = 0;

    public int key = 0;

    /*
    0.2차심장
    1.풀차지
    2.방어력업
    3.방사능주사
    4.위장강화
    5.티타늄이빨
    6.자석꼬리
    7.쿼드코어
    8.고기자석꼬리
    9.전방방패
    10.더미데이터
    13.더미데이터
    14.더미데이터
    15.더미데이터
    16.더미데이터
    17.더미데이터
    18.더미데이터
    19.더미데이터
    20.더미데이터
    21.더미데이터
    22.없음
    23.랜덤
      */

    public int[] evolveLvData;

    public Material material;

    public GameObject magnet;
    public GameObject meatTail;

    public GameObject shield;
    private bool isGameOver = false;
    private bool isRevive = false;
    private Manager manager;
    private PlayerMove playerMove;
    private ParticlePool particlePool;
    public Animator animator;

    public int MagnetLV
    {
        get
        {
            return evolveLvData[7];
        }
        set
        {
            evolveLvData[7] = value;
            magnet.GetComponent<SphereCollider>().radius *= 2f;
        }
    }

    public int MeatTailLV
    {
        get
        {
            return evolveLvData[8];
        }
        set
        {
            evolveLvData[8] = value;
            meatTail.GetComponent<SphereCollider>().radius *= 2f;
        }
    }

    private Coroutine radiantion;
    private bool isradiantion = false;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp += value;
            if (hp >= maxhp)
            {
                hp = maxhp;
            }
            else if (evolveLvData[3] > 0)
            {
                if (!isradiantion)
                {
                    if (radiantion != null)
                        StopCoroutine(radiantion);

                    boostParticle.Play();
                    radiantion = StartCoroutine(RadiationInjectionCoroutine());
                }
            }
        }
    }

    public float Ep
    {
        get
        {
            return ep;
        }
        set
        {
            ep += value + (evolveLvData[7] * 0.4f);
            if (ep >= maxEp)
            {
                epLv++;
                ep = ep - maxEp;
                maxEp = maxEp + epLv * 2;
                manager.Evolution();
            }
        }
    }

    public float Gold
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
        evolveLvData = new int[24] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //FindObjectOfType<hpSlider>().playerData = this;
        playerMove = GetComponent<PlayerMove>();
        FindObjectOfType<SkillSystem>().playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
        magnet = FindObjectOfType<Magnet>().gameObject;
        meatTail = FindObjectOfType<MeatTail>().gameObject;
        meatTail.GetComponent<MeatTail>().SetPlayer(this);
        shield = FindObjectOfType<Shield>().gameObject;
        particlePool = FindObjectOfType<ParticlePool>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        boostParticle = GetComponentInChildren<ParticleSystem>();
        shield.SetActive(false);
        magnet.SetActive(false);
        meatTail.SetActive(false);
        if (!isTest)
        {
            PlayerSetting();
        }
        hp = maxhp;
    }

    public bool isHalfSpeech = false;

    private void PlayerSetting()
    {
        if (smite)
        {
            df = -20;
        }

        var x = UserDataManager.Instance.userData;
        gold = x.Money;
        maxhp = x.hp;
        df = 0;
        var fperHp = (maxhp * 0.05f);
        hpDownSpeed = fperHp - (fperHp * (0.01f * x.decelerationHp));
        hpUpSpeed = hpUpSpeed + (hpUpSpeed * (0.01f * x.healHp));
        epUpSpeed = epUpSpeed + (epUpSpeed * x.gainExp * 0.01f);
        goldUpSpeed = goldUpSpeed + (goldUpSpeed * x.gainMoney * 0.01f);
        var userEquipSkillList = x.skillLVList;
        for (int i = 0; i < userEquipSkillList.Length; i++)
        {
            evolveLvData[i] = userEquipSkillList[i];
        }
    }

    private void Update()
    {
        if (hp <= 0)
        {
            animator.SetBool("Dying", true);
        }

        if (!isGameOver && !isRevive)
        {
            if (hp >= 0)
            {
                hp = hp - hpDownSpeed * Time.deltaTime;
            }
            else if (live > 0)
            {
                StartCoroutine(Revive());
                isRevive = true;
            }
            else
            {
                FindObjectOfType<Manager>().GameOver();
                isGameOver = true;
                return;
            }
        }
    }

    private IEnumerator Revive()
    {
        Time.timeScale = 0.3f;
        playerMove.speed = 1f;
        yield return new WaitForSeconds(1f);
        var a = Physics.OverlapSphere(transform.position, 13f);
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject.layer == LayerMask.NameToLayer("Magnet"))
            {
                continue;
            }
            StartCoroutine(KuckBack(a[i]));
        }
        var particle = particlePool.GetParticle(particlePool.blastParticlePool);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        hp = maxhp * 0.3f;
        live--;
        Time.timeScale = 1;
        animator.SetBool("Dying", false);
        playerMove.speed = 11f;
        isRevive = false;
        yield break;
    }

    private IEnumerator KuckBack(Collider a)
    {
        for (int k = 0; k < 45; k++)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, transform.position, -45f * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    private float originSpeed = 0f;

    private IEnumerator RadiationInjectionCoroutine()

    {
        isradiantion = true;
        if (originSpeed == 0)
            originSpeed = playerMove.speed;
        playerMove.speed *= 1.5f;
        yield return new WaitForSeconds(3f);

        playerMove.speed = 11;
        originSpeed = 0f;
        isradiantion = false;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - evolveLvData[4] - df);
            var hitEffect = particlePool.GetParticle(particlePool.hitParticlePool);
            hitEffect.transform.position = transform.position;
            hitEffect.transform.localRotation = transform.rotation;
            hitEffect.SetActive(true);
        }
        else if (other.tag == "Coin")
        {
            Gold = goldUpSpeed;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Meat")
        {
            var xep = epUpSpeed + (evolveLvData[7]);
            var xhp = hpUpSpeed + (maxhp * (0.03f * evolveLvData[5]));
            Ep = xep;
            hp += xhp;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "RandomBox")
        {
            randomBoxCount++;
        }
    }
}