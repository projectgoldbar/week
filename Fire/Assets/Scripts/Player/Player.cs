using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float MaxHp;

    public ShootEffectPool effect;

    public bool Hit = false;

    public Material mat;

    private WaitForSeconds wait = new WaitForSeconds(0.05f);

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                Debug.Log("플레이어 사망");
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

    private float hp;

    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Hp = MaxHp;

        // mat.color = Ref.Instance.NonColor();
    }

    private void OnApplicationQuit()
    {
        mat.color = Color.white;
    }

    public void Gameover()
    {
        mat.color = Color.white;
        GameManager.instance.GameEnd();
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
            //var PlayerPos = transform.position;
            //var EnemyPos = collision.transform.position;

            //var particle = effect.Geteffect();

            //var dir = (EnemyPos - PlayerPos);

            //particle.transform.position = EnemyPos;
            //particle.transform.rotation = Quaternion.LookRotation(dir.normalized);

            //particle.time = 0;
            //particle.Play();

            Hp -= 1;

            //unit = collision.GetComponent<MonsterUnit>();

            //if (unit.state == StateIndex.ATTACK)
            { StartCoroutine(HitDamageEffectColorBlink()); }
        }
    }
}