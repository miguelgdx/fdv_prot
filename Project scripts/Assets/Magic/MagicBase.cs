using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour
{
    public enum MagicName
    {
        Fireball, Heal
    }

    Camera mainCamera;
    // A percentage of how much offset before dissappearing off-bounds.
    float offLimitOffset = 0f;
    // Is there something to instantiate after colliding?
    public GameObject afterCollisionEffect;
    // What's the skill name of this spell
    public MagicName skill;
    // List of object types that can collide with the magic.
    public List<CustomCollisionProperties.ObjectTypeEnum> collidableTypes;
    // Pointing to self collider.
    protected Collider2D selfCollider;
    // Has the object been collision-consumed?
    protected bool consumed = false; 
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        mainCamera = Camera.main;
        selfCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isOffLimits())
        {
            Destroy(this.gameObject);
        }
    }
    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnCollision()
    {
        Destroy(gameObject);
    }
    // Check if the current gameobject is off-limit from Camera
    bool isOffLimits()
    {
        bool result = false;
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 - offLimitOffset && screenPoint.x > 0 - offLimitOffset && screenPoint.x < 1 + offLimitOffset && screenPoint.y > 0 && screenPoint.y < 1 + offLimitOffset;
        result = !onScreen;
        return result;
    }
    protected void setOffLimitOffset(float x)
    {
        offLimitOffset = x;
    }

}
