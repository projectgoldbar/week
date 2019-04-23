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
        for (int i = 0; i < 2; i++)
        {
            mat.color = Utility.Instance.ColorChageForColorBlink(Color.white, Color.red);
            yield return wait;
        }
    }

    private MonsterUnit unit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            var PlayerPos = transform.position;
            var EnemyPos = collision.transform.position;
            var dir = (EnemyPos - PlayerPos);

            var particle = effect.Geteffect();

            particle.transform.position = EnemyPos;
            particle.transform.rotation = Quaternion.LookRotation(dir.normalized);

            particle.time = 0;
            particle.Play();

            Hp -= 1;

            //unit = collision.GetComponent<MonsterUnit>();

            //if (unit.state == StateIndex.ATTACK)
            { StartCoroutine(HitDamageEffectColorBlink()); }
        }
    }
}