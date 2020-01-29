using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Defines the basic information of a collectable and what happens after being taken
 */
public class Collectable : MonoBehaviour
{
    // Defining types of items
    public enum CollectableName
    {
        Redshard, Blueshard
    }

    public CollectableName item;
    public AudioClip collectSound;
    public float collectSoundVolume = 0.4f;
    public GameObject collectionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // What should happen when an item is collected
    public void onCollection()
    {
        if (item == CollectableName.Redshard || item == CollectableName.Blueshard)
        {
            // Instantiate a Global Sound Source.
            GameObject globalSound = (GameObject)Instantiate(Resources.Load("GlobalSoundSource"));
            GlobalSoundPlay soundScript = globalSound.GetComponent<GlobalSoundPlay>();
            soundScript.playGlobalSound(collectSound, collectSoundVolume);
            Destroy(this.gameObject);
            if (collectionEffect != null)
            {
                GameObject ce = Instantiate(collectionEffect);
                ce.transform.position = this.transform.position;
            }
        }
        
    }
}
