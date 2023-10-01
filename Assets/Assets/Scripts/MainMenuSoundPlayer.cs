using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx;

    public void button()
    {
        src.clip = sfx;
        src.Play();
    }
}
