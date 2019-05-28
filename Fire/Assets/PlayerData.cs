using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject dummyShied;
    public ParticleSystem boostParticle;

    public float maxhp = 50f;
    public float hp = 50f;
    public float hpDownSpeed = 3.3f;
    public float skillLv = 0;
    public float skillCountLv = 0;
    public float maxEp = 10f;
    public float epLv = 0;
    public float ep = 0f;
    public int gold = 0;
    public int df = 0;
    public int live = 0;

    /*
    0.2차심장
    1.풀차지
    2.하늘에서고기가
    3.어미구더기
    4.방어력업
    5.방사능주사
    6.위장강화
    7.티타늄이빨
    8.자석꼬리
    9.쿼드코어
    10.금화추적기
    11.고기자석꼬리
    12.더미데이터
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
            else if (evolveLvData[5] > 0)
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
        evolveLvData = new int[24] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        hp = maxhp;
        //FindObjectOfType<hpSlider>().playerData = this;
        playerMove = GetComponent<PlayerMove>();
        FindObjectOfType<SkillSystem>().playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
        magnet = FindObjectOfType<Magnet>().gameObject;
        shield = FindObjectOfType<Shield>().gameObject;
        particlePool = FindObjectOfType<ParticlePool>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        boostParticle = GetComponentInChildren<ParticleSystem>();
        shield.SetActive(false);
        magnet.SetActive(false);
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
                hp = hp - (hpDownSpeed * (1 - evolveLvData[6] * 0.1f)) * Time.deltaTime;
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
        evolveLvData[4] += evolveLvData[5];
        yield return new WaitForSeconds(3f);

        playerMove.speed = 11;
        evolveLvData[4] -= evolveLvData[5];
        originSpeed = 0f;
        isradiantion = false;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - evolveLvData[4]);
            var hitEffect = particlePool.GetParticle(particlePool.hitParticlePool);
            hitEffect.transform.position = transform.position;
            hitEffect.transform.localRotation = transform.rotation;
            hitEffect.SetActive(true);
        }
        else if (other.tag == "Coin")
        {
            Gold = 3;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Meat")
        {
            var xep = 3f + (evolveLvData[9] * 0.5f);
            var xhp = 5f + (evolveLvData[7] * 3f);
            Ep = xep;
            hp += xhp;
            other.gameObject.SetActive(false);
        }
    }
}