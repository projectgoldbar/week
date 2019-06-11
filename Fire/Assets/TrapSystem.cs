using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSystem : MonoBehaviour
{
    public List<Transform> traps;
    public int numOfTrap = 3;

    private void Awake()
    {
        var x = GetComponentsInChildren<Transform>();
        for (int i = 1; i < x.Length; i++)
        {
            x[i].gameObject.SetActive(false);
            traps.Add(x[i]);
        }
        for (int i = 0; i < numOfTrap; i++)
        {
            SetTrap();
        }
    }

    private void SetTrap()
    {
        var x = Random.Range(0, traps.Count);
        traps[x].gameObject.SetActive(true);
        traps.RemoveAt(x);
    }
}