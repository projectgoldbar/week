using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public GameObject dummyShied;
    public GameObject arrow;
    public GameObject evadeParticle;
    public ParticleSystem boostParticle;
    public bool isTest;
    public float epRecoverSpeed = 1f;
    public Text hpText;
    public Gate gate;
    public int rollStack = 1;

    private bool smite = false;

    public float maxhp = 50f;
    public float hp = 50f;
    public float score = 0;
    public float ep = 10f;
    public float maxEp = 10f;

    /// <summary>
    /// 스킬지속시간관련
    /// </summary>
    public float skillLv = 0;

    /// <summary>
    /// 스킬최대횟수관련
    /// </summary>
    public float skillCountLv = 1;

    public float hpUpSpeed = 5f;
    public float goldUpSpeed = 5;
    public float gold = 0;
    public int df = 0;

    public SphereCollider EvadeCollider;

    #region 퍽 0번 회복력 강화

    private float RecoveryData;
    private int Recovery;
    public int recovery
    {
        get => Recovery;
        set
        {
            Recovery = value;
            if (Recovery == 0) RecoveryData = 0;
            else if (Recovery == 1) RecoveryData = 0.5f;
            else if (Recovery == 2) RecoveryData = 0.75f;
            else if (Recovery == 3) RecoveryData = 1.0f;

            Debug.Log($"회복력강화 -> {Recovery}");
        }
    }

    #endregion

    #region 퍽1번 콩벌레
    public int WormData;
    private int worm;
    public int Worm
    {
        get { return worm; }
        set
        {
            worm = value;
            if (worm == 0) WormData = 0;
            else if (worm == 1) WormData = 2;
            else if (worm == 2) WormData = 4;
            else if (worm == 3) WormData = 7;

            Debug.Log($"콩벌레 -> {WormData}");
        }
    }

    #endregion

    #region 퍽2번 돈벌레
    public float GoldWormData;
    private int goldworm;
    public int GoldWorm
    {
        get { return goldworm; }
        set
        {
            goldworm = value;
            if (goldworm == 0) GoldWormData = 0;
            else if (goldworm == 1) GoldWormData = 0.5f;
            else if (goldworm == 2) GoldWormData = 0.75f;
            else if (goldworm == 3) GoldWormData = 1f;

            Debug.Log($"돈벌레 -> {GoldWormData}");
        }
    }

    #endregion

    #region 퍽3번 지구력
    public float enduranceData;
    private int endurance;
    public int Endurance
    {
        get { return endurance; }
        set
        {
            endurance = value;
            if (endurance == 0) enduranceData = 0;
            else if (endurance == 1) enduranceData = 0.5f;
            else if (endurance == 2) enduranceData = 0.75f;
            else if (endurance == 3) enduranceData = 1f;

            ep += enduranceData;

            Debug.Log($"지구력 -> {enduranceData}");
        }
    }


    #endregion

    #region 퍽4번 제5감각

    public float DefaultEvadeRadius;
    public float SenceData;
    private int sence;
    public int Sence
    {
        get { return sence; }
        set
        {
            sence = value;
            if (sence == 0) SenceData = 0;
            else if (sence == 1) SenceData = 0.5f;
            else if (sence == 2) SenceData = 0.75f;
            else if (sence == 3) SenceData = 1f;

            //회피범위

            var radius = DefaultEvadeRadius + SenceData;
            EvadeCollider.radius = radius;

            Debug.Log($"제5감각 -> {SenceData}");
        }
    }

    #endregion

    #region 퍽5번 재난대처능력
    public float calamityData;
    private int calamity;
    public float MainusEP = 2;
    public int Calamity
    {
        get { return calamity; }
        set
        {
            calamity = value;
            if (calamity == 0) calamityData = 0;
            else if (calamity == 1) calamityData = 0.3f;
            else if (calamity == 2) calamityData = 0.4f;
            else if (calamity == 3) calamityData = 0.5f;


            Debug.Log($"재난대처능력 -> {calamityData}");
        }
    }

    #endregion

    #region 퍽6번 숨쉬기운동
    public float breathingData;
    private int breathing;

    public int Breathing
    {
        get { return breathing; }
        set
        {
            breathing = value;
            if (breathing == 0) breathingData = 0;
            else if (breathing == 1) breathingData = 0.05f;
            else if (breathing == 2) breathingData = 0.1f;
            else if (breathing == 3) breathingData = 0.2f;

            maxhp += (maxhp * breathingData);
            Debug.Log($"숨쉬기운동 -> {breathingData}");
        }
    }

    #endregion

    #region 퍽7번 협상
    public int AddGoldData;
    private int addGold;
    public int AddGold
    {
        get { return addGold; }
        set
        {
            addGold = value;
            if (addGold == 0) AddGoldData = 0;
            else if (addGold == 1) AddGoldData = 2;
            else if (addGold == 2) AddGoldData = 3;
            else if (addGold == 3) AddGoldData = 4;

            Debug.Log($"협상 -> {AddGoldData}");
        }
    }



    #endregion

    #region 퍽8번 질주
    private float DefaultSpeedData;
    public float SpeedRunData;
    private int speedRun;

    public int SpeedRun
    {
        get { return speedRun; }
        set
        {
            speedRun = value;
            if (speedRun == 0) SpeedRunData = 0;
            else if (speedRun == 1) SpeedRunData = 0.3f;
            else if (speedRun == 2) SpeedRunData = 0.35f;
            else if (speedRun == 3) SpeedRunData = 0.4f;


            var data = DefaultSpeedData + SpeedRunData;
            playerMove.maxSpeed = data;

            Debug.Log($"질주 -> {SpeedRunData}");
        }
    }

    #endregion

    public int goldBoxCount = 0;
    public int silverBoxCount = 0;
    public int bronzeBoxCount = 0;
    public int clearCount = 0;
    public int boxBuffer = 0;

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
    public Manager manager;
    private PlayerMove playerMove;
    private ParticlePool particlePool;
    public Animator animator;

    public SkinnedMeshRenderer MeshData;

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
            meatTail.GetComponent<SphereCollider>().radius += 5f;
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
        //FindObjectOfType<SkillSystem>().playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
        //magnet = FindObjectOfType<Magnet>().gameObject;
        //meatTail = FindObjectOfType<MeatTail>().gameObject;
        //meatTail.GetComponent<MeatTail>().SetPlayer(this);
        //shield = FindObjectOfType<Shield>().gameObject;
        particlePool = FindObjectOfType<ParticlePool>();
        animator = GetComponentInChildren<Animator>();

        //boostParticle = GetComponentInChildren<ParticleSystem>();
        //shield.SetActive(false);
        //magnet.SetActive(false);
        //meatTail.SetActive(false);
        if (!isTest)
        {
            PlayerSetting();
            MeshData.sharedMesh = UserDataManager.Instance.EquipSkinReference[UserDataManager.Instance.userData.equipedSkinIdx].sharedMesh;
        }
        hp = maxhp;
        DefaultEvadeRadius = EvadeCollider.radius;
        DefaultSpeedData = playerMove.maxSpeed;
    }


    #region 구르기 애니메이션 이벤트로 넣음.
    public void RollDfUp()
    {
        df += WormData;
        if (playerMove.CalamityRoll)
        {
            df += 1;
        }
    }

    public void RollDfDown()
    {
        df -= WormData;
        if (playerMove.CalamityRoll)
        {
            df -= 1;
            playerMove.CalamityRoll = false;
        }
    }
    #endregion

    private void PlayerSetting()
    {
        if (smite)
        {
            return;
        }

        var x = UserDataManager.Instance.userData;
        gold = x.Money;
        maxhp = x.hp;
        df = 0;
        var fperHp = (maxhp * 0.05f);
        //hpDownSpeed = fperHp - (fperHp * (0.01f * x.decelerationHp));
        hpUpSpeed = hpUpSpeed + (hpUpSpeed * (0.01f * x.healHp));
        goldUpSpeed = goldUpSpeed + (goldUpSpeed * x.gainMoney * 0.01f);
        playerMove.equipIdx = x.equipedSkinIdx;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            //manager.GameOver();
            animator.Play("die");
        }
        if (ep < maxEp)
        {
            ep += epRecoverSpeed * Time.deltaTime;
        }
    }

    private void GameOverInvoke()
    {
        manager.GameOver();
    }

    private IEnumerator Revive()
    {
        Time.timeScale = 0.3f;
        playerMove.speed = 1f;
        yield return new WaitForSeconds(1f);
        var a = Physics.OverlapSphere(transform.position, 13f);
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject.layer == LayerMask.NameToLayer("Magnet") || a[i].gameObject.layer == LayerMask.NameToLayer("MeatTail"))
            {
                continue;
            }
            StartCoroutine(KuckBack(a[i]));
        }
        var particle = particlePool.GetParticle(particlePool.blastParticlePool);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        hp = maxhp * 0.3f;
        recovery--;
        Time.timeScale = 1;
        animator.SetBool("Dying", false);
        animator.SetBool("Revive", false);

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

    public GameObject hitUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - evolveLvData[4] - df);
            var hitEffect = particlePool.GetParticle(particlePool.hitParticlePool);
            if (hitUI.activeSelf)
            {
                hitUI.SetActive(false);
                hitUI.SetActive(true);
            }
            else
            {
                hitUI.SetActive(true);
            }
            hitEffect.transform.position = transform.position;
            hitEffect.transform.localRotation = transform.rotation;
            hitEffect.SetActive(true);
        }
        else if (other.tag == "Coin")
        {
            Gold = goldUpSpeed;
            hp += GoldWormData;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Meat")
        {
            var xhp = hpUpSpeed + (maxhp * (0.03f * evolveLvData[5]));
            var addHp = xhp + (xhp * RecoveryData);
            hp += addHp;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "RandomBox")
        {
            var box = other.GetComponent<Box>();
            switch (box.type)
            {
                case BoxType.Bronze:
                    manager.score += 4000f;
                    bronzeBoxCount++;
                    Destroy(other.gameObject);
                    break;

                case BoxType.Gold:
                    manager.score += 4000f;
                    goldBoxCount++;

                    Destroy(other.gameObject);

                    break;

                case BoxType.Silver:
                    manager.score += 4000f;
                    silverBoxCount++;

                    Destroy(other.gameObject);

                    break;

                default:
                    break;
            }
        }
        else if (other.tag == "Spit")
        {
            Debug.Log("Spit 맞음");
            other.gameObject.SetActive(false);

            var dir = (other.transform.position - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            Hp = -1;

            spitPoolManager.Instance.NoActive(transform.position, rot);
        }
    }
}