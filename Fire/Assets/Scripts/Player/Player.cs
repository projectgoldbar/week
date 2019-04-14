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
                Move.StopMove?.Invoke();
                Anim.SetTrigger("Dead");
                //Invoke("Gameover", 3.0f);
            }
            // else
            //    Ref.Instance.hpbar.value = (Hp / MaxHp);
        }
    }

    private float hp;

    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Hp = MaxHp;

        mat.color = Ref.Instance.NonColor();
    }

    public void Gameover()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator hitDamage()
    {
        for (int i = 0; i <= 2; i++)
        {
            if (i / 2 == 1)
                mat.color = Ref.Instance.NonColor();
            else
                mat.color = Ref.Instance.RedColor();

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

            // Hp -= 1;

            //unit = collision.GetComponent<MonsterUnit>();

            //if (unit.state == StateIndex.ATTACK)
            { StartCoroutine(hitDamage()); }
        }
    }
}