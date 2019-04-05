using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MaxHp;

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
    }

    public void Gameover()
    {
        SceneManager.LoadScene(0);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        Hp -= 1;

    //        Debug.Log(collision.gameObject.name);
    //    }
    //}
}