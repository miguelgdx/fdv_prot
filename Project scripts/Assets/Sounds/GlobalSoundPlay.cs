using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundPlay : MonoBehaviour
{
    public AudioClip soundToPlay;
    AudioSource source;

    public GlobalSoundPlay(AudioClip sound)
    {
        this.soundToPlay = sound;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void playGlobalSound(AudioClip sound, float volume)
    {
        soundToPlay = sound;
        source = GetComponentInParent<AudioSource>();
        source.PlayOneShot(soundToPlay, volume);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (soundToPlay != null && !source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
