using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffectsPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        src.clip = backgroundMusic;
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
     
    }
}
