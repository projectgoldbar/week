using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(2500f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    //   r 매니저에서 가져와야 하는 부분
    public bool checkBGM = true;

    public bool checkSFX = true;
    //   ㄴ 매니저에서 가져와야 하는 부분

    public Image checkBoxBGMImage;
    public Image checkBoxSFXImage;

    public SoundManager soundManager;
    private void Start()
    {
        SoundManager.Instance._isSfxSound = checkSFX;
        SoundManager.Instance._isSfxSound = checkBGM;
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
        SoundManager.Instance._isSfxSound = checkBGM;
        if (checkBGM)
        {
            soundManager.PlaySoundSFX("SoundCheckBox");
        }
        soundManager.BGMSoundOnOff(checkBGM);
    }

    public void OnClickSFX_Button()
    {
        checkSFX = !checkSFX;
        checkBoxSFXImage.enabled = checkSFX;
        SoundManager.Instance._isSfxSound = checkSFX;

        if (checkSFX)
        {
            soundManager.PlaySoundSFX("SoundCheckBox");
        }
        soundManager.SFMSoundOnOff(checkSFX);
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