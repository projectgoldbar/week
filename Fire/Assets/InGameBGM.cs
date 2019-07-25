using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBGM : MonoBehaviour
{
    private SoundManager soundManager;
    public AudioClip[] BGMRoopingClips;

    private int Rnd = 0;

    public static bool IngameBgmOnoff = true;

    AudioClip Clip;

    private void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            var Clips = SoundManager.Instance.SoundDic[$"INGAMEBGM{i}"];
            BGMRoopingClips[i] = Clips;
            SoundManager.Instance.BGMsources[i].clip = BGMRoopingClips[i];
        }

        Rnd = Random.Range(0, 3);

        Clip = BGMRoopingClips[Rnd];

        SoundManager.Instance.BGMsources[Rnd].clip = Clip;

        if(SoundManager.Instance._isBgmSound)
        SoundManager.Instance.BGMsources[Rnd].Play();
    }


    float SoundDelay = 0;

    public void Update()
    {
        if (!IngameBgmOnoff) return;

        SoundDelay += Time.deltaTime;

        if (SoundManager.Instance.BGMsources[Rnd % 3].clip.length <= SoundDelay)
        {
            SoundDelay = 0;
            SoundPlay((++Rnd) % 3);
        }


        //    //플레이중이 아닐때 
        //if (!InGameAudioSource[Rnd % 3].isPlaying)
        //{
        //    SoundPlay((++Rnd) % 3);
        //}
    }

    public void SoundPlay(int rnd)
    {
        SoundManager.Instance.BGMsources[rnd].Play();
    }

    public void BGMSoundOnOff(bool Active)
    {
        if (!Active)
        {
            //for (int i = 0; i < InGameAudioSource.Length; i++)
            //{
            //    InGameAudioSource[i].Stop();
            //}

            SoundManager.Instance.BGMsources[Rnd].Stop();
        }
        else
        {
            Clip = BGMRoopingClips[Rnd];

            SoundManager.Instance.BGMsources[Rnd].clip = Clip;
            SoundManager.Instance.BGMsources[Rnd].Play();
           
        }
    }


}
