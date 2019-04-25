using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public float MaxHp;

    public ShootEffectPool effect = null;

    public bool Hit = false;

    public Material mat;

    private WaitForSeconds wait = new WaitForSeconds(0.05f);

    private float hp;

    private Action specialMove = null;
    private Action setItemOption = null;

    public Button evasionTrigger;

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                //Debug.Log("플레이어 사망");
                GameLevelManager.instance.OnGameOverPanel();

                GetComponent<Move>().rotState = Move.State.DEAD;
                Anim.SetTrigger("Dead");
                Invoke("Gameover", 1.0f);
                Utility.Instance.PlayerHpText.text = "0";
                Utility.Instance.PlayerHpBar.value = 0;
            }
            else
            {
                Utility.Instance.PlayerHpText.text = Mathf.RoundToInt(Hp).ToString();
                Utility.Instance.PlayerHpBar.value = (Hp / MaxHp);
            }
        }
    }

    [NonSerialized]
    private Animator Anim;

    public void StatusRefresh()
    {
    }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Anim.SetFloat("RunSpeed", 1.0f);
        Hp = MaxHp;
        Hp += GameManager.instance.playerHp;
        evasionTrigger.gameObject.SetActive(false);
        // mat.color = Ref.Instance.NonColor();
    }

    private void OnApplicationQuit()
    {
        mat.color = Color.white;
    }

    public void Gameover()
    {
        mat.color = Color.white;
    }

    public IEnumerator HitDamageEffectColorBlink()
    {
        for (int i = 0; i <= 2; i++)
        {
            if (i % 2 == 0)
                mat.color = Utility.Instance.ChangeColor(Color.white);
            else
                mat.color = Utility.Instance.ChangeColor(Color.red);

            // mat.color = Utility.Instance.ColorChageForColorBlink(Color.white, Color.red);
            yield return wait;
        }
    }

    private MonsterUnit unit;

    private bool evasion = false;

    private float evationTimer = 0;

    public ParticleSystem Ex;

    private void Update()
    {
        if (evasion)
        {
            evationTimer += Time.deltaTime;
            if (evationTimer >= 0.3f)
            {
                if (EnemyTr != null)
                    DamageHit(EnemyTr);
                evationTimer = 0;
                evasionTrigger.gameObject.SetActive(false);
                evasion = false;
            }
            else
            {
                //1.버튼
                evasionTrigger.gameObject.SetActive(true);
                //2.스와이프
                //3.화면클릭
            }
        }
    }

    private Transform EnemyTr;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            evationTimer = 0;
            EnemyTr = collision.transform;
            evasion = true;
        }
    }

    //회피
    public void Evasion()
    {
        //예 1)
        Ex.time = 0;
        Ex.Play();
        Hp += 10;
        evasion = false;
        evasionTrigger.gameObject.SetActive(false);
    }

    //데미지히트
    private void DamageHit(Transform CollisionTr)
    {
        var PlayerPos = transform.position;
        var EnemyPos = CollisionTr.position;
        var dir = (EnemyPos - PlayerPos);

        var particle = effect.Geteffect();

        particle.transform.position = PlayerPos;
        particle.transform.rotation = Quaternion.LookRotation(dir.normalized);

        particle.time = 0;
        particle.Play();

        Hp -= 1;

        { StartCoroutine(HitDamageEffectColorBlink()); }
    }
}