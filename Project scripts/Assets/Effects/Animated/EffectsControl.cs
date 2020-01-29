using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip appearSound;
    [Range(0.0f, 1.0f)]
    public float volume = 0.4f;
    void Start()
    {
        GameObject globalSound = (GameObject)Instantiate(Resources.Load("GlobalSoundSource"));
        GlobalSoundPlay soundScript = globalSound.GetComponent<GlobalSoundPlay>();
        soundScript.playGlobalSound(appearSound, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }

}
