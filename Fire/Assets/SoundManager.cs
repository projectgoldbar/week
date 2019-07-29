using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    // public static SoundManager Instance;

    //오디오클립당 오디오소스 1개
    //BGM
    public AudioSource[] BGMsources;

    public AudioSource[] GameSceneBGMsources;

    public static SoundManager Instance;
    //SFM
    public AudioSource[] SFMsources;

   

    public SoundClip[] soundClips;

    public Dictionary<string, AudioClip> SoundDic = new Dictionary<string, AudioClip>();

    public Toggle AllSoundToggle = null;
    public Toggle AllSoundMute = null;
    public Slider SoundVolume = null;

    private OptionPanel option;
    public bool _isSfxSound = true;
    public bool _isBgmSound = true;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        option = FindObjectOfType<OptionPanel>();

        for (int i = 0; i < soundClips.Length; i++)
        {
            SoundDic.Add(soundClips[i].name, soundClips[i].clip);
        }
        DontDestroyOnLoad(gameObject);
        //PlayBGM(SoundDic["mainBGM"], true, 0);
        //PlayBGM(SoundDic["subBGM2"], true, 3);
    }


   
    public void PlaySoundSFX(string SoundName)
    {
        if (_isSfxSound)
            PlaySFM(SoundDic[SoundName], false, 0);
    }

    public void PlaySoundSFX(string SoundName,float delay =0f)
    {
        if (_isSfxSound)
            PlaySFM(SoundDic[SoundName], false, delay);
    }

    public void PlaySoundBGM(string SoundName)
    {
        if (_isBgmSound)
            PlayBGM(SoundDic[SoundName], true, 0);
    }

    public void PlayBGM(AudioClip clip, bool loop, float delay)
    {
        for (int i = 0; i < BGMsources.Length; i++)
        {
            if (!BGMsources[i].isPlaying)
            {
                //Debug.Log("실행");
                BGMsources[i].clip = clip;
                BGMsources[i].loop = loop;
                BGMsources[i].PlayDelayed(delay);
                break;
            }
        }
    }

    public void PlaySFM(AudioClip clip, bool loop, float delay)
    {
        for (int i = 0; i < SFMsources.Length; i++)
        {
            if (!SFMsources[i].isPlaying)
            {
                //.Log("실행");
                SFMsources[i].clip = clip;
                SFMsources[i].loop = loop;
                SFMsources[i].PlayDelayed(delay);
                break;
            }
        }
    }

    #region BGM 정지

    public void BGMSoundOnOffOnclick()
    {
        if (!AllSoundToggle.isOn)
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].Play();
            }
        }
    }

    public void BGMSoundOnOff(bool Active)
    {
        if (!Active)
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].Pause();
            }
        }
        else
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].Play();
            }
        }
    }

    #endregion BGM 정지

    #region SFM 정지

    public void SFMSoundOnOffOnclick()
    {
        if (!AllSoundToggle.isOn)
        {
            for (int i = 0; i < SFMsources.Length; i++)
            {
                SFMsources[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < SFMsources.Length; i++)
            {
                SFMsources[i].Play();
            }
        }
    }

    public void SFMSoundOnOff(bool Active)
    {
        if (!Active)
        {
            for (int i = 0; i < SFMsources.Length; i++)
            {
                SFMsources[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < SFMsources.Length; i++)
            {
                SFMsources[i].Play();
            }
        }
    }

    #endregion SFM 정지

    #region 볼륨

    public void Soundvolume()
    {
        for (int i = 0; i < BGMsources.Length; i++)
        {
            BGMsources[i].volume = SoundVolume.value;
        }
        for (int i = 0; i < SFMsources.Length; i++)
        {
            SFMsources[i].volume = SoundVolume.value;
        }
    }

    #endregion 볼륨

    #region 음소거

    //음소거
    public void SoundMuteOnClick()
    {
        if (!AllSoundMute.isOn)
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].mute = true;
            }
        }
        else
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].mute = false;
            }
        }
    }

    public void SoundMute(bool Active)
    {
        if (!Active)
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].mute = true;
            }
        }
        else
        {
            for (int i = 0; i < BGMsources.Length; i++)
            {
                BGMsources[i].mute = false;
            }
        }
    }

    #endregion 음소거



   


}

[System.Serializable]
public class SoundClip
{
    public string name;
    public AudioClip clip;
}