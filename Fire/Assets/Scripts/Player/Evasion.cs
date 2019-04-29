using System;
using UnityEngine;
using UnityEngine.UI;

public class Evasion : MonoBehaviour
{
    public Player player;

    [Header("Test용 버튼")]
    public Button evasionTrigger;

    [Header("Test용 Particle")]
    public ParticleSystem EvasionEx;

    [System.NonSerialized]
    public float evationTimer = 0;

    //public Transform EnemyTr = null;

    private bool b_evasion = false;

    private Action method;

    private Swipe swipe;

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
        swipe = FindObjectOfType<Swipe>();
        method += EvasionProcess;
        // evasionTrigger.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (b_Evasion)
        {
            evationTimer += Time.deltaTime;
            if (evationTimer >= 0.3f)
            {
                Time.timeScale = 1;
                player.DamageHit();
                evationTimer = 0;
                evasionTrigger.gameObject.SetActive(false);
                b_Evasion = false;
                swipe.GoSwipe = false;
            }
            else
            {
                //1.버튼
                Time.timeScale = 0.5f;
                evasionTrigger.gameObject.SetActive(true);
                //2.스와이프
                swipe.GoSwipe = true;
                //if (swipe.GoSwipe)
                //    swipe.SwipeProcess(() => EvasionProcess());
                //3.화면클릭
            }
        }
    }

    public void EvasionProcess()
    {
        //예 1)
        EvasionEx.transform.position = transform.position;
        b_Evasion = false;

        evationTimer = 0;
        EvasionEx.time = 0;
        EvasionEx.Play();
        player.Hp += 10;
        Time.timeScale = 1;
    }
}