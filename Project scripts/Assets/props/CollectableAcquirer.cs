using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * To be used by those objects that can grab collectables so they perform callbacks
 */
public class CollectableAcquirer : MonoBehaviour
{
    public OnRedShardAcquire onRedShardAcquire;
    public OnBlueShardAcquire onBlueShardAcquire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // What delegate gets executed depending on the item gathered.
    public void onAcquireItem(Collectable.CollectableName item)
    {
        switch (item)
        {
            case Collectable.CollectableName.Redshard:
                onRedShardAcquire();
                break;
            case Collectable.CollectableName.Blueshard:
                onBlueShardAcquire();
                break;
        }
    }

    // Function to be called when red shard is acquired.
    public delegate void OnRedShardAcquire();
    public delegate void OnBlueShardAcquire();
}
