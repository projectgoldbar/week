﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFrontalZombie : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

}