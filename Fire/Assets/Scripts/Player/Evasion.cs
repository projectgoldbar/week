using UnityEngine;
using UnityEngine.UI;

public class Evasion : MonoBehaviour
{
    public Player player;

    public Button evasionTrigger;
    public ParticleSystem EvasionEx;

    [System.NonSerialized]
    public float evationTimer = 0;

    //public Transform EnemyTr = null;

    private bool b_evasion = false;

    public bool b_Evasion
    {
        get
        {
            return b_evasion;
        }
        set
        {
            b_evasion = value;

            if (b_evasion) evasionTrigger.gameObject.SetActive(true);
            else evasionTrigger.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        evasionTrigger.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (b_Evasion)
        {
            evationTimer += Time.deltaTime;
            if (evationTimer >= 0.3f)
            {
                player.DamageHit();
                evationTimer = 0;
                evasionTrigger.gameObject.SetActive(false);
                b_Evasion = false;
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

    public void EvasionProcess()
    {
        //예 1)
        b_Evasion = false;
        EvasionEx.time = 0;
        EvasionEx.transform.position = transform.position;
        EvasionEx.Play();
        player.Hp += 10;
    }
}