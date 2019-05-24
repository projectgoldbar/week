using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Evolve
{
    public string name;
    public string description;
    public Sprite sprite;
    public int idx = 0;
}

public class EvolveSystem : MonoBehaviour
{
    public Evolve[] evolveIdx;

    public List<Evolve> Evolve()
    {
        List<bool> boolIdx = new List<bool>();
        for (int i = 0; i < evolveIdx.Length; i++)
        {
            boolIdx.Add(true);
        }
        List<Evolve> returnValue = new List<Evolve>();

        for (int j = 0; j < 3; j++)
        {
            var x = Random.Range(0, evolveIdx.Length);
            if (boolIdx[x] == true)
            {
                boolIdx[x] = false;
                returnValue.Add(evolveIdx[x]);
            }
            else
            {
                j--;
            }
        }
        return returnValue;
    }
}