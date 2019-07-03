using System.Collections.Generic;
using UnityEngine;

public class EvadeSystem : MonoBehaviour
{
    public Queue<Collider> enemys;
    public Manager manager;
    public PlayerData playerData;
    public PlayerMove playerMove;
    public float startHp;

    private void Awake()
    {
        enemys = new Queue<Collider>();
    }

    private void OnEnable()
    {
        startHp = playerData.Hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemys.Enqueue(other);
        //Debug.Log("트리거에들어온" + enemys.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        enemys.Enqueue(other);
    }

    private void OnDisable()
    {
        if (startHp <= playerData.Hp && enemys.Count > 0)
        {
            Debug.Log("회피성공" + enemys.Count);
            playerData.ep += 2f;
            playerData.evadeParticle.SetActive(true);
            manager.score += enemys.Count;
        }
        //Debug.Log("큐카운트" + enemys.Count);
        enemys.Clear();
    }
}