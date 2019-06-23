using UnityEngine;
using UnityEngine.UI;

public class MiddleAllPanel : MonoBehaviour
{
    private bool isOpened = false;
    private float openPos = 0f;
    private float closePos = 180f;
    private float duration = 1f;

    private Vector2 nowPos = Vector2.zero;

    public void OpenPanel()
    {
        isOpened = true;
        MovePanelTween(isOpened);
    }

    public void ClosePanel()
    {
        isOpened = false;
        MovePanelTween(isOpened);
    }

    private void MovePanelTween(bool now)
    {
        if (now == true)
        {
            //열어
            LTDescr d = LeanTween.value(gameObject, closePos, openPos, duration).setEase(LeanTweenType.easeOutBack);
            d.setOnUpdate(x => { MoveUpdatePanel(x); });
            d.setOnComplete(MovePanelTweenCompleteOpen);
        }
        else
        {
            //닫아
            LTDescr d = LeanTween.value(gameObject, openPos, closePos, duration).setEase(LeanTweenType.easeOutBack);
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
        isOpened = true;
    }

    private void MovePanelTweenCompleteOpen()
    {
        isOpened = false;
    }
}