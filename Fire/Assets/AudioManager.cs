using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip idleAudio;
    public AudioClip lv1Audio;
    public AudioClip finalleAudio;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LV1AudioPlay()
    {
        StartCoroutine(SoundChangeSystem(lv1Audio));
    }

    public void LV2AudioPlay()
    {
        StartCoroutine(SoundChangeSystem(finalleAudio));
    }

    public void LV3AudioPlay()
    {
    }

    private IEnumerator SoundChangeSystem(AudioClip ac)
    {
        audioSource.loop = false;

        for (; ; )
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = ac;
                audioSource.Play();
                audioSource.loop = true;
                yield break;
            }
            yield return null;
        }
    }
}