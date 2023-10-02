using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPool : MonoBehaviour
{
    private AudioSource audioSource;

    public List<AudioClip> clipList;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFromPool()
    {
        audioSource.clip = clipList[Random.Range(0, clipList.Count)];
        audioSource.Play();
    }

    public void PlaySoundFromPoolPitch(float pitch)
    {
        audioSource.pitch = pitch;
        PlaySoundFromPool();
    }

    public void PlaySoundFromPoolVolume(float volume)
    {
        audioSource.volume = volume;
        PlaySoundFromPool();
    }
}
