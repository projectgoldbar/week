using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject[] buildings;
    public TileGenerator tileGenerator;

    private void Start()
    {
        Building();
    }

    private void Building()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject.Instantiate(buildings[Random.Range(0, buildings.Length)], tileGenerator.xyMap[i, j].transform);
            }
        }
    }
}