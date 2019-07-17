using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public GameObject dummyShied;
    public GameObject evadeParticle;
    public GameObject ATFieldUI;
    public ParticleSystem shieldParticle;
    public ParticleSystem healingParticle;
    public ParticleSystem clearParticle;//wave
    public ParticleSystem smackParticle;
    public float originEpRecoverSpeed = 1f;
    public float epRecoverSpeed = 1f;
    public Text hpText;
    public Gate gate;

    [Header("스킨인덱스")]
    public int equipSkinIdx = 0;

    public int randomGold = 0;

    [Header("구르기필요변수")]
    public float rollEp = 3f;

    private float calamityrollEp = 5f;
    private float originEp = 7f; // 원래 돌떄 필요한 포인트

    public float breathingHp = 0.02f;

    public float maxhp = 50f;
    public float hp = 50f;
    public float score = 0;
    public float ep = 10f;
    public float maxEp = 10f;
    public int addHealPackCount = 0;
    public bool isTutirial;
    public bool overHp = false;

    /// <summary>
    /// 스킬지속시간관련
    /// </summary>
    public float skillLv = 0;

    /// <summary>
    /// 스킬최대횟수관련
    /// </summary>
    public float skillCountLv = 1;

    public float hpUpSpeed = 30f;
    public float goldUpSpeed = 5;
    public float gold = 0;
    public int df = 0;

    public SphereCollider EvadeCollider;

    public float RndTimeMin = 3;
    public float RndTimeMax = 20;
    public Queue<GameObject> biteZombies;

    #region 퍽부분

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

    #endregion 퍽 0번 회복력 강화

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

    #endregion 퍽1번 콩벌레

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

    #endregion 퍽2번 돈벌레

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
            epRecoverSpeed += enduranceData;
            Debug.Log($"지구력 -> {enduranceData}");
        }
    }

    #endregion 퍽3번 지구력

    #region 퍽4번 제5감각

    public float SenceData;
    private int sence;

    public int Sence
    {
        get { return sence; }
        set
        {
            sence = value;
            if (sence == 0) SenceData = 0;
            else if (sence == 1) SenceData = 2f;
            else if (sence == 2) SenceData = 3f;
            else if (sence == 3) SenceData = 4f;

            //회피범위

            Debug.Log($"제5감각 -> {SenceData}");
        }
    }

    #endregion 퍽4번 제5감각

    #region 퍽5번 재난대처능력

    public float calamityData;
    private int calamity;
    public float MainusEP = 2;
    public float calamityHp;

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

            calamityHp = maxhp * calamityData;
            Debug.Log($"재난대처능력 -> {calamityData}");
        }
    }

    #endregion 퍽5번 재난대처능력

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

            breathingHp += (maxhp * breathingData);
            Debug.Log($"숨쉬기운동 -> {breathingData}");
        }
    }

    #endregion 퍽6번 숨쉬기운동

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
            goldUpSpeed += AddGoldData;
            Debug.Log($"협상 -> {AddGoldData}");
        }
    }

    #endregion 퍽7번 협상

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
            else if (speedRun == 1) SpeedRunData = 2f;
            else if (speedRun == 2) SpeedRunData = 1f;
            else if (speedRun == 3) SpeedRunData = 1f;

            //var data = DefaultSpeedData + SpeedRunData;
            playerMove.maxSpeed += SpeedRunData;

            Debug.Log($"질주 -> {SpeedRunData}");
        }
    }

    #endregion 퍽8번 질주

    #endregion 퍽부분

    public int goldBoxCount = 0;
    public int silverBoxCount = 0;
    public int bronzeBoxCount = 0;
    public int clearCount = 0;

    public int[] evolveLvData;

    public Material material;

    public GameObject magnet;
    public GameObject meatTail;

    public GameObject shield;
    public bool isGameOver = false;
    private bool isRevive = false;
    public Manager manager;
    private PlayerMove playerMove;
    private ParticlePool particlePool;
    public Animator animator;

    public SkinnedMeshRenderer MeshData;

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
            if (value < 0)
            {
                if (equipSkinIdx == 9)
                {
                    var randomValue = Random.Range(0, 11);
                    if (randomValue > 8)
                    {
                        ATFieldUI.SetActive(true);
                    }
                    else
                    {
                        hp = hp + (WormData + value);
                        PlayerHitEffect();
                    }
                }
                else
                {
                    hp = hp + (WormData + value);
                    ep += sence;
                    PlayerHitEffect();
                    var x = Random.Range(0, 3);
                    if (x == 0)
                    {
                        SoundManager.Instance.PlaySoundSFX("SCREAM0");
                    }
                    else if (x == 1)
                    {
                        SoundManager.Instance.PlaySoundSFX("SCREAM1");
                    }
                    else
                    {
                        SoundManager.Instance.PlaySoundSFX("SCREAM2");
                    }
                }
            }
            else
            {
                hp = hp + value;
                if (hp > maxhp * 1.4f)
                {
                    hp = maxhp * 1.4f;
                }
            }

            if (!overHp && hp > maxhp )
            {
                hp = maxhp;
                
            }
            

            if (hp <= calamityHp)
            {
                rollEp = calamityrollEp;
            }
            else
            {
                rollEp = originEp;
            }
        }
    }

    private float Shuffle(float x)
    {
        x -= randomGold;
        randomGold = Random.Range(2, 15329);
        x += randomGold;
        return x;
    }

    private float UnShuffle(float x)
    {
        x = x - randomGold;
        return x;
    }

    public float Gold
    {
        get
        {
            return UnShuffle(gold);
        }
        set
        {
            if (UserDataManager.Instance.userData.goldBonus)
            {
                var x = gold + (value * 5);
                gold = Shuffle(x);
            }
            else
            {
                var x = gold + value;
                gold = Shuffle(x);
            }
            Hp = (goldUpSpeed * GoldWormData);
            manager.goldUi.text = Gold.ToString();
        }
    }

    private void Awake()
    {
        evolveLvData = new int[24] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
        particlePool = FindObjectOfType<ParticlePool>();
        animator = GetComponentInChildren<Animator>();
        randomGold = Random.Range(4000, 9576);
        gold += randomGold;
        biteZombies = new Queue<GameObject>();
        PlayerSetting();
        MeshData.sharedMesh = UserDataManager.Instance.EquipSkinReference[UserDataManager.Instance.userData.equipedSkinIdx].sharedMesh;
        if (!isTutirial)
        {
            hp = maxhp;
        }
        else
        {
            hp = 10;
        }
        DefaultSpeedData = playerMove.maxSpeed;
        manager.goldUi.text = Gold.ToString();
        if (equipSkinIdx == 10)
        {
            overHp = true;
        }
        else if (equipSkinIdx == 3)
        {
            Debug.Log("3번스킨");

            StartCoroutine(NurseCoroutine());
        }
        else if (equipSkinIdx == 1)
        {
            playerMove.accelSpeed = 11f;
            playerMove.downSpeed = 11f;
        }

        originEp = rollEp;
        calamityrollEp = originEp - 2f;
    }

    private IEnumerator NurseCoroutine()
    {
        WaitForSeconds second = new WaitForSeconds(5f);
        while (true)
        {
            if (Hp < maxhp && ep > 1f)
            {
                Hp = 5f;
                ep -= 3f;

                if (!healingParticle.isPlaying) healingParticle.Play();
                yield return null;
            }
            else
            {
                if (healingParticle.isPlaying) healingParticle.Stop();
                yield return null;
            }
            yield return second;
        }
    }

    private void PlayerSetting()
    {
        var x = UserDataManager.Instance.userData;
        var u = UserDataManager.Instance.UnShuffle(x.Money);
        UserDataManager.Instance.randomValue = 0f;
        x.Money = u;
        Gold = u;
        maxhp = x.hp;
        maxEp = x.ep;
        df = 0;
        var fperHp = (maxhp * 0.05f);
        hpUpSpeed = hpUpSpeed + (hpUpSpeed * (0.01f * x.healHp));
        epRecoverSpeed = originEpRecoverSpeed + (0.01f * x.healEp);
        goldUpSpeed = goldUpSpeed + (goldUpSpeed * x.gainMoney * 0.01f);
        if (x.goldBonus)
        {
            goldUpSpeed *= 5f;
        }
        equipSkinIdx = x.equipedSkinIdx;
        playerMove.equipIdx = x.equipedSkinIdx;
    }

    public bool is_Wall = false;
    private float CurrentTime = 0;

    private float sheildTime = 5f;
    private float sheildCurrentTime;

    private void Update()
    {
        if (isTutirial)
        {
            if (hp <= 5)
            {
                hp = maxhp;
            }
            if (!playerMove.accel)
                Hp = Time.deltaTime;
        }

        if (hp <= 0)
        {
            if (!isGameOver)
            {
                isGameOver = true;
                ep = 0f;
                maxEp = 0f;
                animator.SetBool("Dying", true);
                animator.StopPlayback();
                animator.Play("die");
            }
        }

        if (ep < maxEp)
        {
            ep += epRecoverSpeed * Time.deltaTime;
        }
        else
        {
            ep = maxEp;
        }
    }

    private void GameOverInvoke()
    {
        manager.GameOver();
    }

    #region 사용하지않는 기능들

    //private IEnumerator Revive()
    //{
    //    Time.timeScale = 0.3f;
    //    playerMove.speed = 1f;
    //    yield return new WaitForSeconds(1f);
    //    var a = Physics.OverlapSphere(transform.position, 13f);
    //    for (int i = 0; i < a.Length; i++)
    //    {
    //        if (a[i].gameObject.layer == LayerMask.NameToLayer("Magnet") || a[i].gameObject.layer == LayerMask.NameToLayer("MeatTail"))
    //        {
    //            continue;
    //        }
    //        StartCoroutine(KuckBack(a[i]));
    //    }
    //    var particle = particlePool.GetParticle(particlePool.blastParticlePool);
    //    particle.transform.position = transform.position;
    //    particle.SetActive(true);
    //    hp = maxhp * 0.3f;
    //    recovery--;
    //    Time.timeScale = 1;
    //    animator.SetBool("Dying", false);
    //    animator.SetBool("Revive", false);

    //    playerMove.speed = 11f;
    //    isRevive = false;
    //    yield break;
    //}

    //private IEnumerator KuckBack(Collider a)
    //{
    //    for (int k = 0; k < 45; k++)
    //    {
    //        a.transform.position = Vector3.MoveTowards(a.transform.position, transform.position, -45f * Time.deltaTime);
    //        yield return null;
    //    }

    //    yield break;
    //}

    //private float originSpeed = 0f;

    //private IEnumerator RadiationInjectionCoroutine()

    //{
    //    originSpeed = playerMove.maxSpeed;

    //    playerMove.maxSpeed *= 1.5f;
    //    yield return new WaitForSeconds(1f);

    //    playerMove.maxSpeed = originSpeed;
    //    originSpeed = 0f;
    //    yield break;
    //}

    #endregion 사용하지않는 기능들

    public GameObject hitUI;

    private float EpHitData = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            if (equipSkinIdx == 8) //에너지쉴드 스킨
            {
                if (ep + EpHitData > 0)
                {
                    ep -= EpHitData;
                    shieldParticle.Play();
                }
                else
                {
                    Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - df);
                }
            }
            else
            {
                Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - df);
            }
        }
        else if (other.tag == "Coin")
        {
            SoundManager.Instance.PlaySoundSFX("GAINCOIN");
            Gold = goldUpSpeed;

            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Meat")
        {
            SoundManager.Instance.PlaySoundSFX("GAINGOGI");
            var xhp = hpUpSpeed + (maxhp * (0.03f));
            var addHp = xhp + (xhp * RecoveryData);
            Hp = addHp;
            addHealPackCount++;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "RandomBox")
        {
            SoundManager.Instance.PlaySoundSFX("GAINBOX");
            var box = other.GetComponent<Box>();
            switch (box.type)
            {
                case BoxType.Bronze:
                    if (!isTutirial)
                    {
                        manager.score += 4000f;
                    }
                    bronzeBoxCount++;
                    clearParticle.Play();
                    for (; 0 < biteZombies.Count;)
                    {
                        var x = biteZombies.Dequeue();
                        x.transform.parent = null;
                        x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
                    }
                    SoundManager.Instance.PlaySoundSFX("STAGEBOMB", 1f);
                    Destroy(other.gameObject);
                    break;

                case BoxType.Gold:
                    if (!isTutirial)
                    {
                        manager.score += 4000f;
                    }
                    goldBoxCount++;
                    clearParticle.Play();
                    for (; 0 < biteZombies.Count;)
                    {
                        var x = biteZombies.Dequeue();
                        x.transform.parent = null;
                        x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
                    }
                    SoundManager.Instance.PlaySoundSFX("STAGEBOMB", 1f);

                    Destroy(other.gameObject);

                    break;

                case BoxType.Silver:
                    if (!isTutirial)
                    {
                        manager.score += 4000f;
                    }
                    silverBoxCount++;
                    clearParticle.Play();
                    for (; 0 < biteZombies.Count;)
                    {
                        var x = biteZombies.Dequeue();
                        x.transform.parent = null;
                        x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
                    }
                    SoundManager.Instance.PlaySoundSFX("STAGEBOMB", 1f);

                    Destroy(other.gameObject);

                    break;

                default:
                    break;
            }
            if (equipSkinIdx == 5)
            {
                Gold = 50f + (50f * FindObjectOfType<StageManager>().currentStageLV);
            }
        }
        else if (other.tag == "BiteZombie")
        {
            if (biteZombies.Count < 10)
            {
                other.GetComponent<ZombieState.ZombieRunBite>().ChangeBite();
                other.transform.SetParent(this.transform, true);
                biteZombies.Enqueue(other.gameObject);
            }
            else
            {
                Hp = -1 * (other.GetComponent<ZombieState.ZombiesComponent>().damage - df);
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

    public void PlayerHitEffect()
    {
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
}