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

    
    private void Start()
    {
        checkBGM = SoundManager.Instance._isBgmSound;    //   ㄴ 매니저에서 가져와야 하는 부분
        checkSFX = SoundManager.Instance._isSfxSound; //   ㄴ 매니저에서 가져와야 하는 부분


        if (checkBGM)
        {
            for (int i = 0; i < SoundManager.Instance.BGMsources.Length; i++)
            {
                SoundManager.Instance.BGMsources[i].clip = null;
                SoundManager.Instance.BGMsources[i].Stop();
            }

            SoundManager.Instance.BGMsources[0].clip =
                SoundManager.Instance.SoundDic["LOBBYBGM"];

            SoundManager.Instance.BGMsources[0].Play();
        }


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
        SetCheckBox();
        SoundManager.Instance._isBgmSound = checkBGM;

        if (checkBGM)
        {
            SoundManager.Instance.PlaySoundSFX("SoundCheckBox");
        }
        SoundManager.Instance.BGMSoundOnOff(checkBGM);
    }

    public void OnClickSFX_Button()
    {
        checkSFX = !checkSFX;
        SetCheckBox();
        SoundManager.Instance._isSfxSound = checkSFX;

        if (checkSFX)
        {
            SoundManager.Instance.PlaySoundSFX("SoundCheckBox");
        }
        //SoundManager.Instance.SFMSoundOnOff(checkSFX);  
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