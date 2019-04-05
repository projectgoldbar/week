using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetData : MonoBehaviour
{
    public TextAsset data;

    public Dictionary<string, string> dataSet = new Dictionary<string, string>();

    private string[] value;

    private void Start()
    {
        Debug.Log(data.text);

        value = data.text.Split(',');

        save();
    }

    private void save()
    {
        for (int i = 1; i <= value.Length / 2; i++)
        {
        }
    }

    private void Read()
    {
    }
}