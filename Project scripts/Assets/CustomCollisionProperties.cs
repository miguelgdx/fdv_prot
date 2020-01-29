using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionProperties : MonoBehaviour
{
    public bool canCollideCharacter = false;
    public bool canCollideTerrain = false;
    private Collider2D selfCollider;
    public List<ObjectTypeEnum> physicCollisions;

    public enum ObjectTypeEnum
    {
        Character, Terrain, NPC, Magic, Etc, CameraCollisions, DestructibleProp, CollectableItem, Enemy
    }

    public ObjectTypeEnum ObjectType = 0;
    // Start is called before the first frame update
    void Start()
    {
        scanCollisions();
    }

    private void Awake()
    {
        selfCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool collidesWithTerrain()
    {
        return canCollideTerrain;
    }

    public bool collidesWithCharacter()
    {
        return canCollideCharacter;
    }

    // Clear all collisions, setting 'em all to false.
    public void clearAllColliders()
    {
        canCollideCharacter = false;
        canCollideTerrain = false;
    }

    public ObjectTypeEnum getObjectType()
    {
        return ObjectType;
    }


    public void scanCollisions()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
            if (go.activeInHierarchy)
                checkCollisionable(go);
    }

    public void checkCollisionable(GameObject g)
    {
        CustomCollisionProperties otherProp = g.GetComponent<CustomCollisionProperties>();
        Collider2D oc = g.GetComponent<Collider2D>();

        if (oc == null || otherProp == null)
            return;

        
        if (otherProp != null)
        {
            //Debug.Log("Checking: " + oc.gameObject + " from script: " + otherProp);
            if (physicCollisions.Contains(otherProp.getObjectType()) || otherProp.physicCollisions.Contains(getObjectType()))
            {
                //OnCollision();
            }
            else
            {
                Physics2D.IgnoreCollision(selfCollider, oc);
            }
        }
        else
        {
            //Physics2D.IgnoreCollision(selfCollider, oc);
        }
    }

}
