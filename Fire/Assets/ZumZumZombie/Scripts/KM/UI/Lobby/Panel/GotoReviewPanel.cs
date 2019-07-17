using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoReviewPanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(5000f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    public void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void OpenPanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }
}