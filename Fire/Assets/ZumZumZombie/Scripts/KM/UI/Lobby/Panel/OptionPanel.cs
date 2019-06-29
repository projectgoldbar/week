using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(2500f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    //   r 매니저에서 가져와야 하는 부분
    private bool checkBGM = true;

    private bool checkSFX = true;
    //   ㄴ 매니저에서 가져와야 하는 부분

    public Image checkBoxBGMImage;
    public Image checkBoxSFXImage;

    private void Start()
    {
        SetCheckBox();
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void OpenPanel()
    {
        SetCheckBox();
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    // r 이부분 매니저에서 불러야 할껄?
    public void OnClickBGM_Button()
    {
        checkBGM = !checkBGM;
        checkBoxBGMImage.enabled = checkBGM;
    }

    public void OnClickSFX_Button()
    {
        checkSFX = !checkSFX;
        checkBoxSFXImage.enabled = checkSFX;
    }

    // ㄴ 이부분 매니저에서 불러야 할껄?

    private void SetCheckBox()
    {
        if (checkBGM == true)
        {
            checkBoxBGMImage.enabled = true;
        }
        else
        {
            checkBoxBGMImage.enabled = false;
        }

        if (checkSFX == true)
        {
            checkBoxSFXImage.enabled = true;
        }
        else
        {
            checkBoxSFXImage.enabled = false;
        }
    }
}