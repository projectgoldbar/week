using System.Collections.Generic;
using UnityEngine;

public enum ClipIndex { Explosion, 정해야됨2, 정해야됨3, 정해야됨4 }

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //오디오클립당 오디오소스 1개
    public AudioSource mainBgm;

    public AudioSource subBgm;

    public AudioSource trapBgm;

    public SoundClip[] soundClips;

    public Dictionary<string, AudioClip> SoundDic = new Dictionary<string, AudioClip>();

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

        PlayBGM(mainBgm, SoundDic["mainBGM"], true, 0);
        PlayBGM(subBgm, SoundDic["subBGM2"], true, 3);
    }

    public void PlayBGM(AudioSource audioSource, AudioClip clip, bool loop, float delay)
    {
        Debug.Log("실행");
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.PlayDelayed(delay);
    }

    //public void SoundPlay(int index)
    //{
    //    audioSources[index % audioSources.Length].PlayOneShot(clips[index % clips.Length]);
    //}

    //테스트해봐야됨.
    //public void SoundPlay2(int index2)
    //{
    //    SoundDic[index2 % clips.Length].audioSource.PlayOneShot(SoundDic[index2 % clips.Length].clip);
    //}

    //public void ExplosionSfx()
    //{
    //    audioSources[(int)ClipIndex.Explosion].PlayOneShot(clips[(int)ClipIndex.Explosion]);
    //}

    //public void 정해야됨2()
    //{
    //    audioSources[(int)ClipIndex.정해야됨2].PlayOneShot(clips[(int)ClipIndex.정해야됨2]);
    //}

    //public void 정해야됨3()
    //{
    //    audioSources[(int)ClipIndex.정해야됨3].PlayOneShot(clips[(int)ClipIndex.정해야됨3]);
    //}

    //public void 정해야됨4()
    //{
    //    audioSources[(int)ClipIndex.정해야됨4].PlayOneShot(clips[(int)ClipIndex.정해야됨4]);
    //}
}

[System.Serializable]
public class SoundClip
{
    public string name;
    public AudioClip clip;
}