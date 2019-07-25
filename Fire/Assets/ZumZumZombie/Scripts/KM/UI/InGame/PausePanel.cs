using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(2500f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    private bool checkBGM;
    private bool checkSFX;

    public Image checkBoxBGMImage;
    public Image checkBoxSFXImage;

    public InGameBGM inGameBGM;
    

    private void Start()
    {
        checkBGM = SoundManager.Instance._isBgmSound;    //   ㄴ 매니저에서 가져와야 하는 부분
        checkSFX = SoundManager.Instance._isSfxSound; //   ㄴ 매니저에서 가져와야 하는 부분

        Debug.Log($"checkBGM{checkBGM}");
        Debug.Log($"checkSFX{checkSFX}");
       

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
        SoundManager.Instance._isBgmSound = checkBGM;
        InGameBGM.IngameBgmOnoff = checkBGM;
        if (SoundManager.Instance._isBgmSound)
        {
            SoundManager.Instance.PlaySoundSFX("SoundCheckBox");
        }
        inGameBGM.BGMSoundOnOff(checkBGM);
    }

    public void OnClickSFX_Button()
    {
        checkSFX = !checkSFX;
        checkBoxSFXImage.enabled = checkSFX;
        SoundManager.Instance._isSfxSound = checkSFX;

        if (checkSFX)
        {
            SoundManager.Instance.PlaySoundSFX("SoundCheckBox");
        }

        SoundManager.Instance.SFMSoundOnOff(checkSFX);
    }

    // ㄴ 이부분 매니저에서 불러야 할껄?

    public void GiveUp()
    {
        //게임 포기하기
        Debug.Log("게임포기하기");
    }

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