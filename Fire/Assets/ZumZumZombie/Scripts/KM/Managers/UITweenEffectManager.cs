using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweenEffectManager : MonoBehaviour
{
    private static UITweenEffectManager instance;

    public static UITweenEffectManager Instace
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public StageOpenPanel stageOpenPanel;
}