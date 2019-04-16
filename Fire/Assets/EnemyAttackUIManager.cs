using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyAttackUIManager : MonoBehaviour
{
    public static EnemyAttackUIManager instance;

    public Sprite fallDownZombieUI;
    public List<RectTransform> uiRectTransform;
    public Camera uiCamera;

    private void Awake()
    {
        instance = this;
    }

    public void Draw(ZombieType type, float drawTime, Vector3 pointFromWorld)
    {
        var rectTransform = GetEmptyRectTransform();
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

    private RectTransform GetEmptyRectTransform()
    {
        RectTransform a = null;
        for (int i = 0; i < uiRectTransform.Count; i++)
        {
            if (uiRectTransform[i].gameObject.activeSelf == false)
            {
                a = uiRectTransform[i];
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
        rect.gameObject.SetActive(false);
    }

    private IEnumerator Timer(Coroutine drawUI, float time, RectTransform rect)
    {
        yield return new WaitForSeconds(time);
        StopCoroutine(drawUI);
        rect.gameObject.SetActive(false);
        yield break;
    }
}