using System;
using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-900)]
public class Player : MonoBehaviour
{
    public float MaxHp;
    public ShootEffectPool effect = null;
    public bool Hit = false;
    public Material mat;

    private WaitForSeconds wait = new WaitForSeconds(0.05f);
    private Action specialMove = null;
    private Action setItemOption = null;

    [NonSerialized]
    public Animator Anim;

    private MonsterUnit unit;

    private float hp;

    public Evasion evasion;

    private void Start()
    {
    }

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
                //StartCoroutine(HpGageSmoth());
                Utility.Instance.PlayerHpText.text = Mathf.RoundToInt(hp).ToString();
                Utility.Instance.PlayerHpBar.value = (Hp / MaxHp);
            }
        }
    }

    private IEnumerator HpGageSmoth()
    {
        var HPG = Utility.Instance.PlayerHpBar.value * 10;

        while (true)
        {
            var Current = Utility.Instance.PlayerHpBar.value;

            Current = Mathf.Lerp(Current, hp / MaxHp, Time.deltaTime * 5.0f);
            Utility.Instance.PlayerHpBar.value = Current;
            Utility.Instance.PlayerHpText.text = Mathf.RoundToInt(hp).ToString();
            Debug.Log("a");

            yield return null;
        }
    }

    public void StatusRefresh()
    {
    }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Anim.SetFloat("RunSpeed", 1.0f);
        Hp = MaxHp;
        //Hp += GameManager.instance.playerHp;

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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            //if (evasion != null)
            //{
            //    evasion.b_Evasion = true;
            //}

            DamageHit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interaction"))
        {
            other.GetComponent<Interaction>().Somthing();
        }
    }

    //데미지히트
    public void DamageHit()
    {
        var PlayerPos = transform.position;

        var particle = effect.Geteffect();

        particle.transform.position = PlayerPos;

        particle.time = 0;
        particle.Play();

        Hp -= 1;

        { StartCoroutine(HitDamageEffectColorBlink()); }
    }
}