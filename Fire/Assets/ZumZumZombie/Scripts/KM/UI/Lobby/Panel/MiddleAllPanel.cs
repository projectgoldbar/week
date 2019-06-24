using UnityEngine;
using UnityEngine.UI;

public class MiddleAllPanel : MonoBehaviour
{
    public bool isOpened = false;
    private float openPos = 0f;
    private float closePos = 1000f;
    private float duration = 0.5f;

    private Vector2 nowPos = Vector2.zero;

    private void Start()
    {
        isOpened = true;
    }

    public void OpenPanel()
    {
        if (isOpened == true)
        {
            ScalePanelTween();
            return;
        }
        isOpened = true;
        MovePanelTween(isOpened);
    }

    private void ScalePanelTween()
    {
        Debug.Log("openning : cant reOpen");

        LeanTween.scale(gameObject.GetComponent<RectTransform>(), Vector3.one * 1.2f, 0.1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBack);
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

    public void ClosedPanelwhenEnter()
    {
        isOpened = false;
        Vector2 ClosedPos = new Vector2(0f, closePos);
        gameObject.GetComponent<RectTransform>().anchoredPosition = ClosedPos;
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
        isOpened = false;
    }

    private void MovePanelTweenCompleteOpen()
    {
        isOpened = true;
    }
}