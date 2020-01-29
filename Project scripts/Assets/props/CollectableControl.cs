using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * All logic related to how the collectable item interacts with the world.
 */
public class CollectableControl : MonoBehaviour
{
    Rigidbody2D rb;
    float appearForceMult = 10.0f;
    private Collectable collectableScript;
    public List<CustomCollisionProperties.ObjectTypeEnum> collecters;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        collectableScript = gameObject.GetComponent<Collectable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        // Push in a random direction
        Vector2 appearDir = new Vector2(Random.Range(-0.3f, 0.3f) + 0.1f, 1.2f);
        rb.AddForce(appearDir * appearForceMult, ForceMode2D.Impulse);
    }

    // Check if the character has collected the item.
    // This is called thanks to 2nd collider (trigger)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the collision collisionant type (if any).
        CustomCollisionProperties ccp = collision.gameObject.GetComponent<CustomCollisionProperties>();
        if (ccp != null && collecters.Contains(ccp.ObjectType))
        {
            // Tell Collectable to execute the onCollection script.
            collectableScript.onCollection();
            // Tell the other object they took the item.
            GameObject acquirer = collision.gameObject;
            // Search for the CollectableAcquirer script
            CollectableAcquirer caScript = acquirer.GetComponent<CollectableAcquirer>();
            if (caScript != null)
            {
                // Let the collectable acquirer script execute the delegates.
                caScript.onAcquireItem(collectableScript.item);
            }
        }
        
    }
}
