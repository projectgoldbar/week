using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObjController : MonoBehaviour
{
    private Vector3 appearScale;
    private Vector3 disAppearScale;
    private float disAppearScaleZ = -0.01f;
    private float panelMoveDuration = 0.5f;

    private void Awake()
    {
        appearScale = gameObject.transform.localScale;
        disAppearScale = new Vector3(appearScale.x, appearScale.y, disAppearScaleZ);
    }

    private void Start()
    {
        gameObject.transform.localScale = disAppearScale;
    }

    public void OpenMiddlePanel()
    {
        AppearStartObjTween();
    }

    public void CloseMiddlePanel()
    {
        DisAppearStartObjTween();
    }

    private void AppearStartObjTween()
    {
        LeanTween.scaleZ(gameObject, appearScale.z, panelMoveDuration).setEase(LeanTweenType.easeInCubic);
    }

    private void DisAppearStartObjTween()
    {
        LeanTween.scaleZ(gameObject, disAppearScale.z, panelMoveDuration).setEase(LeanTweenType.easeInBack);
    }
}