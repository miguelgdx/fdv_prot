using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioClip castingSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCasting()
    {
        audioSource.PlayOneShot(castingSound, 0.2f);
    }

    public void stopSounds()
    {
        audioSource.Stop();
    }

}
