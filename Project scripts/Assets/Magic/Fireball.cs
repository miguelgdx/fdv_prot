using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MagicBase
{
    public Vector2 target;
    public float speed = 1f;
    public Vector2 direction;
    public Vector2 startPosition;
    public GameObject trailEffectSystem;
    public AudioClip appearSound;
    public GameObject fireballBurstEffect;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.position = startPosition;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        direction = (target - origin).normalized;
        // We instantiate any trail system (particle emitter).
        trailEffectSystem = (GameObject)Instantiate(trailEffectSystem, transform);
        // Set the trail to point in the opposite direction
        trailEffectSystem.transform.position = transform.position;
        trailEffectSystem.transform.rotation = transform.rotation;
        // 20% screen margin to be off-limits.
        setOffLimitOffset(0.2f);

        // Instantiate a Global Sound Source.
        GameObject globalSound = (GameObject)Instantiate(Resources.Load("GlobalSoundSource"));
        GlobalSoundPlay soundScript = globalSound.GetComponent<GlobalSoundPlay>();
        soundScript.playGlobalSound(appearSound, 0.4f);
        skill = MagicBase.MagicName.Fireball;


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // What should happen when a collision is triggered
    protected override void OnCollision()
    {
        base.OnCollision();
        // Instantiate after-collision effect
        afterCollisionEffect = (GameObject)Instantiate(afterCollisionEffect);
        afterCollisionEffect.transform.position = transform.position;
        fireballBurstEffect = (GameObject)Instantiate(fireballBurstEffect);
        fireballBurstEffect.transform.position = transform.position;
    }

    // If the magic enters in contact with another collider
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the other object has a "CustomCollisionProperties" script.
        CustomCollisionProperties otherProp = collider.gameObject.GetComponent<CustomCollisionProperties>();
        Debug.Log("La magia colisiona con: " + otherProp.gameObject);
        if (otherProp != null)
        {
            if (collidableTypes.Contains(otherProp.getObjectType()) && !consumed)
            {
                consumed = true;
                OnCollision();
                // Tell the other gameobject to apply any magic effect against them.
                applyMagicEffect(collider);
            }
            else
                Physics2D.IgnoreCollision(selfCollider, collider);
        }
        else
        {
            Physics2D.IgnoreCollision(selfCollider, collider);
        }
    } 

    // Applies whatever effect the magic has against the other object.
    private void applyMagicEffect(Collider2D co)
    {
        // Check if the other object has a OnMagicImpact
        GameObject go = co.gameObject;
        OnMagicImpact script = go.GetComponent<OnMagicImpact>();
        if (script != null)
        {
            Debug.Log("Lanzando delegado");
            OnMagicImpact.ImpactDetails details = new OnMagicImpact.ImpactDetails();
            details.skillName = skill;
            details.hitPoint = this.transform.position;

            script.triggerDelegate(details);
        }
    }
}
