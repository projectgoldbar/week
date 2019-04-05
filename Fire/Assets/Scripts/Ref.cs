using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ref : Singleton<Ref>
{
    [Header("Player")]
    public Slider hpbar = null;

    public Transform playerTr = null;
}