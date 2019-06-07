using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyAttackUIManager : MonoBehaviour
{
    public static EnemyAttackUIManager instance;

    public Sprite fallDownZombieUI;
    public Sprite testRushUI;
    public List<RectTransform> uiRectTransform;
    public List<RectTransform> rushUIlist;
    public Camera uiCamera;

    private void Awake()
    {
        instance = this;
    }

    public void Draw(ZombieType type, float drawTime, Vector3 pointFromWorld)
    {
        var rectTransform = GetEmptyRectTransform(uiRectTransform);
        rectTransform.gameObject.SetActive(true);
        switch (type)
        {
            case ZombieType.Gallery:

                break;

            case ZombieType.Stoker:

                break;

            case ZombieType.Falldown:
                var a = StartCoroutine(DrawUI(fallDownZombieUI, pointFromWorld, rectTransform));
                StartCoroutine(Timer(a, drawTime, rectTransform));
                break;

            default:
                break;
        }
    }

    public void Draw(float drawTime, Vector3 pointFromWorld, Vector3 targetPosition, Vector3 fromPosition, float c)
    {
        var rectTransform = GetEmptyRectTransform(rushUIlist);
        var b = StartCoroutine(DrawRushUI(fallDownZombieUI, pointFromWorld, targetPosition, fromPosition, rectTransform));
        rectTransform.localEulerAngles = RotatePointerTowardsTargetPosition(targetPosition, fromPosition);
        rectTransform.localScale = new Vector3(rectTransform.localScale.x * c, rectTransform.localScale.y, rectTransform.localScale.z);
        StartCoroutine(Timer(b, drawTime, rectTransform));
        rectTransform.gameObject.SetActive(true);
    }

    private RectTransform GetEmptyRectTransform(List<RectTransform> rectList)
    {
        RectTransform a = null;
        for (int i = 0; i < rectList.Count; i++)
        {
            if (rectList[i].gameObject.activeSelf == false)
            {
                a = rectList[i];
            }
        }
        return a;
    }

    private IEnumerator DrawUI(Sprite uiImage, Vector3 pointFromWorld, RectTransform rect)
    {
        rect.GetComponent<Image>().sprite = uiImage;
        while (true)
        {
            var position = Camera.main.WorldToScreenPoint(pointFromWorld);
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(position);
            rect.position = pointerWorldPosition;
            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DrawRushUI(Sprite uiImage, Vector3 pointFromWorld, Vector3 targetPosition, Vector3 fromPosition, RectTransform rect)
    {
        while (true)
        {
            var position = Camera.main.WorldToScreenPoint(pointFromWorld);
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(position);
            rect.position = pointerWorldPosition;
            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);

            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 RotatePointerTowardsTargetPosition(Vector3 targetPosition, Vector3 fromPosition)
    {
        Vector3 toPosition = targetPosition;
        fromPosition.y = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        //this.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
        return new Vector3(0, 0, angle);
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private IEnumerator Timer(Coroutine drawUI, float time, RectTransform rect)
    {
        yield return new WaitForSeconds(time);
        StopCoroutine(drawUI);
        rect.localScale = new Vector3(2, 2, 1);
        rect.gameObject.SetActive(false);
        yield break;
    }
}