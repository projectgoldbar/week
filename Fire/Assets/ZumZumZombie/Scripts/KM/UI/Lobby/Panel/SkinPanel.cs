using UnityEngine;

public class SkinPanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(0f, 2500f);
    private Vector2 openePos = new Vector2(0f, 0f);

    private void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }
}