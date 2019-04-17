using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerSystem : MonoBehaviour
{
    public static MarkerSystem instance;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite indicationSprite;
    [SerializeField] private Sprite indicationSprite2;
    [SerializeField] private Sprite goalMarkSprite;
    [SerializeField] private Sprite homeSprite;
    public Transform target;
    public List<TargetPointer> pointerList;

    private void Awake()
    {
        instance = this;
        SetupNewPointer(target.transform.position);
    }

    public void targetChange(Vector3 newTarget)
    {
        for (int i = 0; i < pointerList.Count; i++)
        {
            if (pointerList[i].gameObject.activeSelf == true)
            {
                pointerList[i].targetPosition = newTarget;
                pointerList[i].indicationSprite = indicationSprite;
                pointerList[i].goalMarkSprite = homeSprite;
            }
        }
    }

    public void SetupNewPointer(Vector3 newTarget)
    {
        for (int i = 0; i < pointerList.Count; i++)
        {
            if (pointerList[i].gameObject.activeSelf == false)
            {
                pointerList[i].targetPosition = newTarget;
                pointerList[i].indicationSprite = indicationSprite;
                pointerList[i].goalMarkSprite = goalMarkSprite;
                pointerList[i].gameObject.SetActive(true);
            }
        }
    }
}