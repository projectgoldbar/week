using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-1000)]
public class TestManager : MonoBehaviour
{
    public NavMeshAgent zombie;
    public NavMeshAgent player;
    public NewMonsterGenerator generator;
    public List<GameObject> gameObjects;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }

    public void SetZombieSpeed(float x)
    {
        zombie.speed = x;
    }

    public void SetZombieRotateSpeed(float x)
    {
        zombie.angularSpeed = x;
    }

    public void SetInstanceZombieCount(int x)
    {
        generator.maxZombieCount = x;
    }

    public void SetPlayerSpeed(float x)
    {
        player.speed = x;
    }

    public void SetPlayerRotateSpeed(float x)
    {
        player.angularSpeed = x;
    }

    public void SetPlayerHp(int x)
    {
        player.GetComponent<Player>().MaxHp = x;
    }
}