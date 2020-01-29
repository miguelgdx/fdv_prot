using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemChance
    {
        public GameObject item;
        [Range(0.0f, 1.0f)]
        public float chance;
    }

    public List<ItemChance> spawnableItems;
    public int maxItemsToSpawn;
    public int minItemsToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem()
    {
        // how many items to spawn.
        int itemsToSpawn = Random.Range(minItemsToSpawn, maxItemsToSpawn);

        for (int i = 0; i < itemsToSpawn; i++)
        {
            // Get the item to spawn.
            GameObject go = generateItem();
            if (go != null)
            {
                go.transform.position = gameObject.transform.position;
                // Check if go has CollectableControl.
                CollectableControl ccScript = go.GetComponent<CollectableControl>();
                ccScript.spawn();
            }
            
        }
    }

    private GameObject generateItem()
    {
        GameObject result = null;

        /*

        float sumOfChances = 0f;
        float nothingChance = 0f;

        foreach (ItemChance it in spawnableItems)
            sumOfChances += it.chance;

        nothingChance = 1f - sumOfChances; */
        List<int> weights = new List<int>();
        foreach (ItemChance it in spawnableItems)
        {
            weights.Add((int) (it.chance * 10));
        }

        int weightR = GetRandomWeightedIndex(weights.ToArray());

        // Instantiate the object from the index.
        GameObject goRef = spawnableItems[weightR].item;
        if (goRef != null)
        {
            result = Instantiate(goRef);
        }

        return result;
    }

    public int GetRandomWeightedIndex(int[] weights)
    {
        if (weights == null || weights.Length == 0) return -1;

        int i;
        int total = 0;
        for (i = 0; i < weights.Length; i++)
        {
            if (weights[i] >= 0)
                total += weights[i];
        }

        float r = Random.value;
        float s = 0f;

        for (i = 0; i < weights.Length; i++)
        {
            if (weights[i] <= 0f) 
                continue;

            s += (float) weights[i] / total;
            
            if (s >= r) 
                return i;
        }

        return -1;
    }
}
