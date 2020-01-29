using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyBase, OnMagicImpact.Effect
{
    public GameObject onDestroyEffect;
    OnMagicImpact magicImpactScript;
    Animator animator;
    SpriteRenderer rend;
    float transparency = 1.0f;
    bool isDying = false;
    float t = 0;
    Rigidbody2D rb;
    // Min distance to start chasing.
    public float distanceToChase = 10f;
    bool isAttacking = false;
    GameObject player;
    bool isLookingLeft = true;
    SpriteRenderer[] childrenRend;
    float limitLookAngle = 40;
    Vector3 returnPoint = Vector3.zero;
    Vector3 attackPoint = Vector3.zero;
    public float chaseSpeed = 4f;
    public float damageAmount = 10f;
    public AudioClip hitSoundEffect;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        magicImpactScript = GetComponent<OnMagicImpact>();
        if (magicImpactScript != null)
            magicImpactScript.effectDelegate = OnMagicImpactEffect;

        animator = GetComponentInChildren<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        //rb.bodyType = RigidbodyType2D.Static;
        player = GameObject.FindGameObjectWithTag("Player");
        childrenRend = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // If rigidbody is active, push upwards a little
        if (stats.status == EnemyStatus.StatusType.idle)
        {
            //rb.AddForce(new Vector2(0f, 1f) * 10);
        }
        if (stats.status != EnemyStatus.StatusType.bouncing)
        {
            rb.freezeRotation = false;
        }

        if (stats.status == EnemyStatus.StatusType.bouncing)
        {
            // Brake bouncing speed.
            rb.AddForce(rb.velocity * -1);
            Debug.DrawRay(transform.position, rb.velocity * -1, Color.yellow);
            rb.freezeRotation = true;
        }

        // If chasing, but the speed is actually opposite to the character dir, get it to zero faster (checking by angle).
        if (stats.status == EnemyStatus.StatusType.chasing)
        {
            Vector2 velocityDir = rb.velocity;
            Vector2 goalPos = player.transform.position;
            Vector2 curPos = transform.position;
            Vector2 goalDir = goalPos - curPos;

            float angle = Vector2.Angle(velocityDir, goalDir);

            // Get the angle
            if (angle > 40)
            {
                rb.velocity = rb.velocity - (rb.velocity * 0.05f);
            }
        }

        if (isDying)
        {
            t += Time.deltaTime * 4f;
            transparency = Mathf.Lerp(1.0f, 0.0f, t);
            foreach (SpriteRenderer r in childrenRend)
            {
                r.color = new Color(1.0f, 1.0f, 1.0f, transparency);
            }
            if (transparency == 0.0f)
                Destroy(gameObject);
            
        }
        else
        {
            if (stats.status != EnemyStatus.StatusType.bouncing)
            {
                // If we're not attacking, check every 2 seconds the player position.
                if (!isAttacking)
                    InvokeRepeating("checkPlayer", 1, 2);
                else
                    attackPlayer();
            }
            
        }
        
    }

    public void OnMagicImpactEffect(OnMagicImpact.ImpactDetails details)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Calculate hit force direction.
        Vector2 batPos = this.transform.position;
        Vector2 hitDir = batPos - details.hitPoint;

        rb.AddForce(hitDir.normalized * 200);
        animator.SetTrigger("onDamage");
        animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        isDying = true;


        //Destroy(gameObject);
        onDestroyEffect = GameObject.Instantiate(onDestroyEffect);
        onDestroyEffect.transform.position = this.transform.position;
    }

    public void attackPlayer()
    {
        // Check the player is not dead anymore.
        CharacterDefinitions cdScript = player.GetComponent<CharacterDefinitions>();
        if (cdScript.stats.status == CharacterDefinitions.CharacterStats.StatusTypes.dead)
        {
            isAttacking = false;
        }
        else {
            // Get player pos.
            Vector2 playerPos = player.transform.position;
            if (playerPos.x < this.transform.position.x)
                lookLeft();
            else
                lookRight();

            lookAt(playerPos);
            attackPoint = playerPos;
            ChasePoint();
        }

    }

    private void checkPlayer()
    {
        if (stats.status == EnemyStatus.StatusType.bouncing)
            return;
        Vector2 playerPos = player.transform.position;
        Vector2 batPos = this.transform.position;
        float distance = Vector2.Distance(playerPos, batPos);
        if (distance < distanceToChase)
        {
            isAttacking = true;
            stats.status = EnemyStatus.StatusType.chasing;
        }
        else
        {
            isAttacking = false;
        }

            
    }

    // Make the bat look at a position.
    private void lookAt(Vector2 pos)
    {
        Vector2 batPos = this.transform.position;
        Vector2 dir = (pos - batPos).normalized;
        float newAngle = 0f;
        Vector3 newRot = this.transform.eulerAngles;
        Vector2 referenceDir = Vector2.zero;

        // If object is looking at the left of the position.
        if (isLookingLeft)
        {
            referenceDir = new Vector2(-1f, 0f);
            
        }
        else
        {
            referenceDir = new Vector2(1f, 0f);
        }

        newAngle = Vector2.SignedAngle(referenceDir, dir);

        // Limit how much the bat rotates.
        if (newAngle > limitLookAngle)
            newAngle = limitLookAngle;
        else if (newAngle < -limitLookAngle)
            newAngle = -limitLookAngle;

        newRot.z = newAngle;
        this.transform.eulerAngles = newRot;

    }

    private void lookLeft()
    {
        if (!isLookingLeft)
        {
            flip();
            //flipSprites();
            isLookingLeft = true;
        }
    }

    private void lookRight()
    {
        if (isLookingLeft)
        {
            flip();
            //flipSprites();
            isLookingLeft = false;
        }
    }

    private void flipSprites()
    {
        foreach (SpriteRenderer r in childrenRend)
        {
            r.flipX = !r.flipX;
        }
    }

    private void flip()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x = currentScale.x * -1;
        this.transform.localScale = currentScale;
    }
    private void ChasePoint()
    {
        Vector3 dir = (attackPoint - this.transform.position).normalized;
        Debug.DrawRay(transform.position, dir, Color.red);
        //this.transform.Translate(dir * Time.deltaTime * chaseSpeed, Space.World);
        currentVelocity = rb.velocity;
        Vector3 maxVelocity = dir * movSpeed;
        float magnitudeDifference = maxVelocity.magnitude - currentVelocity.magnitude;

        // Magnitude difference will make the maxSpeed - currentSpeed thing.
        // Acceleration Factor will have everything faster.
        rb.AddForce(maxVelocity * magnitudeDifference * accelerationFactor * Time.deltaTime);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        // Check if we collided with the player.
        if (collision.gameObject == player && stats.status != EnemyStatus.StatusType.bouncing)
        {
            rb.velocity = Vector2.zero;
            stats.status = EnemyStatus.StatusType.bouncing;
            //Send the hit signal.
            OnEnemyHit.HitDetails hitDetails = new OnEnemyHit.HitDetails();
            hitDetails.damageAmount = damageAmount;
            hitDetails.hitPoint = transform.position;
            hitDetails.hitSfx = hitSoundEffect;

            OnEnemyHit oehScript = player.GetComponent<OnEnemyHit>();
            oehScript.triggerDelegate(hitDetails);
            BounceBack();
        }
    }

    private void BounceBack()
    {
        bounceTimeStart = Time.time;
        // Bounce dir = opposite of char.
        Vector2 pos = this.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 bounceDir = (pos - playerPos).normalized;

        // Get rb normal for a moment.
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(bounceDir * bounceSpeed);
        //this.transform.Translate(bounceDir * bounceSpeed * Time.deltaTime, Space.World);
        //returnPosition = this.transform.position;
    }

}
