﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public GameObject dummyShied;
    public GameObject arrow;
    public GameObject evadeParticle;
    public ParticleSystem boostParticle;
    public ParticleSystem clearParticle;
    public float originEpRecoverSpeed = 1f;
    public float epRecoverSpeed = 1f;
    public Text hpText;
    public Gate gate;
    public int rollStack = 1;
    public int equipSkinIdx = 0;
    public float rollEp = 7f;
    private float calamityrollEp = 5f;
    private float originEp = 7f;
    public float breathingHp = 0f;

    public float maxhp = 50f;
    public float hp = 50f;
    public float score = 0;
    public float ep = 10f;
    public float maxEp = 10f;

    public bool isTest;
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

            breathingHp = (maxhp * breathingData);
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
            else if (speedRun == 1) SpeedRunData = 0.3f;
            else if (speedRun == 2) SpeedRunData = 0.35f;
            else if (speedRun == 3) SpeedRunData = 0.4f;

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
    public int boxBuffer = 0;

    public int key = 0;

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

    //public int MagnetLV
    //{
    //    get
    //    {
    //        return evolveLvData[7];
    //    }
    //    set
    //    {
    //        evolveLvData[7] = value;
    //        magnet.GetComponent<SphereCollider>().radius *= 2f;
    //    }
    //}

    //public int MeatTailLV
    //{
    //    get
    //    {
    //        return evolveLvData[8];
    //    }
    //    set
    //    {
    //        evolveLvData[8] = value;
    //        meatTail.GetComponent<SphereCollider>().radius += 5f;
    //    }
    //}

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
                hp = hp + (WormData + value);
                PlayerHitEffect();
            }
            else
            {
                hp = hp + value;
            }

            if (!overHp && hp > maxhp)
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
        playerMove = GetComponent<PlayerMove>();
        manager = FindObjectOfType<Manager>();
        particlePool = FindObjectOfType<ParticlePool>();
        animator = GetComponentInChildren<Animator>();
        biteZombies = new Queue<GameObject>();

        if (!isTest)
        {
            PlayerSetting();
            MeshData.sharedMesh = UserDataManager.Instance.EquipSkinReference[UserDataManager.Instance.userData.equipedSkinIdx].sharedMesh;
        }
        hp = maxhp;
        DefaultEvadeRadius = EvadeCollider.radius;
        DefaultSpeedData = playerMove.maxSpeed;
        manager.goldUi.text = gold.ToString();
        RndTime = Random.Range(RndTimeMin, RndTimeMax);
        if (equipSkinIdx == 9)
        {
            overHp = true;
        }
    }

    private void PlayerSetting()
    {
        var x = UserDataManager.Instance.userData;
        gold = x.Money;
        maxhp = x.hp;

        df = 0;
        var fperHp = (maxhp * 0.05f);
        hpUpSpeed = hpUpSpeed + (hpUpSpeed * (0.01f * x.healHp));
        epRecoverSpeed = originEpRecoverSpeed + (0.01f * x.healEp);
        goldUpSpeed = goldUpSpeed + (goldUpSpeed * x.gainMoney * 0.01f);
        playerMove.equipIdx = x.equipedSkinIdx;
    }

    public bool is_Wall = false;
    private float RndTime;
    private float CurrentTime = 0;

    private float sheildTime = 5f;
    private float sheildCurrentTime;

    private void Update()
    {
        if (hp <= 0)
        {
            animator.Play("die");
        }

        if (ep < maxEp)
        {
            ep += epRecoverSpeed * Time.deltaTime;
        }

        if (playerMove.equipIdx == 8)
        {
            if (CurrentTime >= RndTime)
            {
                is_Wall = true;
                if (sheildCurrentTime >= sheildTime)
                {
                    sheildCurrentTime = 0;
                    CurrentTime = 0;
                    is_Wall = false;
                }
                else
                {
                    sheildCurrentTime += Time.deltaTime;
                }
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }

    private void GameOverInvoke()
    {
        manager.GameOver();
    }

    #region 사용하지않는 기능들

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

    #endregion 사용하지않는 기능들

    public GameObject hitUI;

    private float EpHitData = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (playerMove.equipIdx == 8)
        {
            if (other.tag == "Zombie" || other.tag == "Spit")
            {
                if (is_Wall) return;
            }
        }

        if (other.tag == "Zombie")
        {
            var zombieData = other.GetComponent<ZombieState.ZombiesComponent>();
            if (zombieData.stateMachine.currentState == zombieData.zombieBite)
            {
                other.transform.SetParent(this.transform, true);
            }

            if (playerMove.equipIdx == 7) //에너지쉴드 스킨
            {
                if (ep + EpHitData > 0)
                {
                    ep -= EpHitData;
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
            Gold = goldUpSpeed;
            Hp = (goldUpSpeed * GoldWormData);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Meat")
        {
            var xhp = hpUpSpeed + (maxhp * (0.03f));
            var addHp = xhp + (xhp * RecoveryData);
            Hp = addHp;
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
                    clearParticle.Play();
                    Destroy(other.gameObject);
                    break;

                case BoxType.Gold:
                    manager.score += 4000f;
                    goldBoxCount++;
                    clearParticle.Play();
                    Destroy(other.gameObject);

                    break;

                case BoxType.Silver:
                    manager.score += 4000f;
                    silverBoxCount++;
                    clearParticle.Play();
                    Destroy(other.gameObject);

                    break;

                default:
                    break;
            }
        }
        else if (other.tag == "BiteZombie")
        {
            if (biteZombies.Count < 4)
            {
                other.GetComponent<ZombieState.ZombieRunBite>().ChangeBite();
                other.transform.SetParent(this.transform, true);
                biteZombies.Enqueue(other.gameObject);
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