using UnityEngine;
using UnityEngine.UI;

public class MiddleAllPanel : MonoBehaviour
{
    public bool isOpened = true;
    private float openPos = 0f;
    private float closePos = 200f;
    private float duration = 0.8f;

    private Vector2 nowPos = Vector2.zero;

    private void Start()
    {
        isOpened = true;
    }

    public void OpenPanel()
    {
        if (isOpened == true)
        {
            Debug.Log("openning : cant reOpen");

            return;
        }
        isOpened = true;
        MovePanelTween(isOpened);
    }

    public void ClosePanel()
    {
        if (isOpened == false)
        {
            Debug.Log("closing : cant reClose");

            return;
        }
        isOpened = false;
        MovePanelTween(isOpened);
    }

    private void MovePanelTween(bool now)
    {
        if (now == true)
        {
            //열어
            LTDescr d = LeanTween.value(gameObject, closePos, openPos, duration).setEase(LeanTweenType.easeInOutBack);
            d.setOnUpdate(x => { MoveUpdatePanel(x); });
            d.setOnComplete(MovePanelTweenCompleteOpen);
        }
        else
        {
            //닫아
            LTDescr d = LeanTween.value(gameObject, openPos, closePos, duration).setEase(LeanTweenType.easeInOutBack);
            d.setOnUpdate(x => { MoveUpdatePanel(x); });
            d.setOnComplete(MovePanelTweenCompleteClose);
        }
    }

    private void MoveUpdatePanel(float value)
    {
        nowPos.y = value;
        gameObject.GetComponent<RectTransform>().anchoredPosition = nowPos;
    }

    private void MovePanelTweenCompleteClose()
    {
        isOpened = false;
    }

    private void MovePanelTweenCompleteOpen()
    {
        isOpened = true;
    }
}