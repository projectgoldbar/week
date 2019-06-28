using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ClipIndex { Explosion, 정해야됨2, 정해야됨3, 정해야됨4 }

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //오디오클립당 오디오소스 1개

    public AudioSource[] sources;

    public SoundClip[] soundClips;

    public Dictionary<string, AudioClip> SoundDic = new Dictionary<string, AudioClip>();


    public Toggle AllSoundToggle;
    public Toggle AllSoundMute;
    public Slider SoundVolume;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        for (int i = 0; i < soundClips.Length; i++)
        {
            SoundDic.Add(soundClips[i].name, soundClips[i].clip);
        }
        PlayBGM(SoundDic["mainBGM"], true, 0);
        PlayBGM(SoundDic["subBGM2"], true, 3);
    }

    public void PlayBGM(AudioClip clip, bool loop, float delay)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            if (!sources[i].isPlaying)
            {
                Debug.Log("실행");
                sources[i].clip = clip;
                sources[i].loop = loop;
                sources[i].PlayDelayed(delay);
                break;
            }
        }
    }

    public void AllSoundOnOff()
    {
        if (!AllSoundToggle.isOn)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Play();
            }
        }
    }

   

    public void BGMSoundvolume()
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = SoundVolume.value;
        }
    }

    //음소거
    public void SoundMute()
    {
        if (!AllSoundMute.isOn)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].mute = true;
            }
        }
        else
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].mute = false;
            }
        }
    }





}

[System.Serializable]
public class SoundClip
{
    public string name;
    public AudioClip clip;
}