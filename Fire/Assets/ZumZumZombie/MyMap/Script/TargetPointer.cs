﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetPointer : MonoBehaviour
{
    public Camera uiCamera;
    public Sprite indicationSprite;
    public Sprite goalMarkSprite;

    public Transform player;
    public Vector3 targetPosition;
    public RectTransform pointerRectTransform;
    public Image pointerImage;
    public RectTransform rectTransform;
    public Text text;

    public float borderSize = 100f;

    private WaitForSeconds second = new WaitForSeconds(0.3f);

    private void OnEnable()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        var x = GetComponent<Image>();
        var a = x.color;
        for (int i = 30; i > 0; i--)
        {
            x.color = a;
            yield return second;
            x.color = Color.yellow;
            yield return second;
        }
        x.color = a;
        yield break;
    }



    private void Update()
    {
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        //if (isOffScreen)
        //{
        RotatePointerTowardsTargetPosition();

        //pointerImage.sprite = indicationSprite;
        ////Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        //cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
        //cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);
        //Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        //pointerRectTransform.position = pointerWorldPosition;
        //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        //}
        //else
        //{
        //pointerImage.sprite = goalMarkSprite;
        //Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        //pointerRectTransform.position = pointerWorldPosition;
        //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        //pointerRectTransform.localEulerAngles = Vector3.zero;
        //}
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = player.transform.position;
        fromPosition.y = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        this.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void Show(Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        this.targetPosition = targetPosition;
    }
}