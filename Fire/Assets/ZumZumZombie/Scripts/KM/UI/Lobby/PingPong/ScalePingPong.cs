using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePingPong : MonoBehaviour
{
    private void OnEnable()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.6f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        //LeanTween.alpha(gameObject, 0f, 0.6f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
    }
}