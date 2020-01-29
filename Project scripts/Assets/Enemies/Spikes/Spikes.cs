using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damageAmount = 5;
    public AudioClip hitSoundEffect;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        // Check if we collided with the player.
        if (collision.gameObject == player)
        {
            InvokeRepeating("continousHitSignal", 0, 1);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Check if we collided with the player.
        if (collision.gameObject == player)
        {
            //Send the hit signal.
            CancelInvoke();
        }
    }

    private void continousHitSignal()
    {
        OnEnemyHit.HitDetails hitDetails = new OnEnemyHit.HitDetails();
        hitDetails.damageAmount = damageAmount;
        hitDetails.hitPoint = transform.position;
        hitDetails.hitSfx = hitSoundEffect;

        OnEnemyHit oehScript = player.GetComponent<OnEnemyHit>();
        oehScript.triggerDelegate(hitDetails);
    }
}
