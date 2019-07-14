using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBGM : MonoBehaviour
{
    private SoundManager soundManager;
    public AudioSource[] InGameAudioSource;
    public AudioClip[] BGMRoopingClips;

    private int Rnd = 0;

    

   

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var Clips = SoundManager.Instance.SoundDic[$"INGAMEBGM{i}"];
            BGMRoopingClips[i] = Clips;
            InGameAudioSource[i].clip = BGMRoopingClips[i];
        }

        Rnd = Random.Range(0, 3);

        var Clip = BGMRoopingClips[Rnd];

        InGameAudioSource[Rnd].clip = Clip;
        InGameAudioSource[Rnd].Play();
    }

    public void Update()
    {
        //플레이중이 아닐때 
        if (!InGameAudioSource[Rnd].isPlaying)
        {
            SoundPlay((++Rnd) % 3);
        }
    }

    public void SoundPlay(int rnd)
    {
        InGameAudioSource[rnd].Play();
    }




}
