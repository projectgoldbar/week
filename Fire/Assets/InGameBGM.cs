using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBGM : MonoBehaviour
{
    private SoundManager soundManager;


    public AudioSource InGameAudioSource;

    public AudioClip[] BGMRoopingClips;

    private int Rnd = 0;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var Clips = SoundManager.Instance.SoundDic[$"INGAMEBGM{i}"];
            BGMRoopingClips[i] = Clips;
        }



        //var Clip0 = SoundManager.Instance.SoundDic["INGAMEBGM0"];
        //BGMRoopingClips[0] = Clip0;

        //var Clip1 = SoundManager.Instance.SoundDic["INGAMEBGM1"];
        //BGMRoopingClips[1] = Clip1;

        //var Clip2 = SoundManager.Instance.SoundDic["INGAMEBGM2"];
        //BGMRoopingClips[2] = Clip2;



        //BGMRoopingClips = soundManager.BGMRoopingClips;
        Rnd = Random.Range(0, 3);

        var Clip = BGMRoopingClips[Rnd];

        InGameAudioSource.clip = Clip;
        InGameAudioSource.Play();
    }



    public void Update()
    {
        //플레이중이 아닐때 
        if (!InGameAudioSource.isPlaying)
        {
            var rnd2 = (Rnd++) % 3;
            SoundPlay(rnd2);
        }
    }


    public void SoundPlay(int rnd)
    {
        var Clip = BGMRoopingClips[rnd];
        InGameAudioSource.clip = Clip;
        InGameAudioSource.Play();
    }




}
