using System.Collections.Generic;
using UnityEngine;

public enum ClipIndex { Explosion, 정해야됨2, 정해야됨3, 정해야됨4 }

public class SoundManager : MonoBehaviour
{
    //오디오클립당 오디오소스 1개
    public AudioSource[] audioSources;

    public AudioClip[] clips;

    public SoundClip[] soundClip;

    public Dictionary<int, SoundClip> SoundDic = new Dictionary<int, SoundClip>();

    private void Awake()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            SoundClip soundClip = new SoundClip();
            soundClip.SoundName = "";
            soundClip.clip = clips[i];

            SoundDic.Add(i, soundClip);
        }
    }

    public void SoundPlay(int index)
    {
        audioSources[index % audioSources.Length].PlayOneShot(clips[index % clips.Length]);
    }

    //테스트해봐야됨.
    public void SoundPlay2(int index2)
    {
        SoundDic[index2 % clips.Length].audioSource.PlayOneShot(SoundDic[index2 % clips.Length].clip);
    }

    public void ExplosionSfx()
    {
        audioSources[(int)ClipIndex.Explosion].PlayOneShot(clips[(int)ClipIndex.Explosion]);
    }

    public void 정해야됨2()
    {
        audioSources[(int)ClipIndex.정해야됨2].PlayOneShot(clips[(int)ClipIndex.정해야됨2]);
    }

    public void 정해야됨3()
    {
        audioSources[(int)ClipIndex.정해야됨3].PlayOneShot(clips[(int)ClipIndex.정해야됨3]);
    }

    public void 정해야됨4()
    {
        audioSources[(int)ClipIndex.정해야됨4].PlayOneShot(clips[(int)ClipIndex.정해야됨4]);
    }
}

[System.Serializable]
public class SoundClip
{
    public string SoundName;
    public AudioSource audioSource;
    public AudioClip clip;
}