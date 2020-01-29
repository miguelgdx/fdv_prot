using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyHit : MonoBehaviour
{
    public class HitDetails
    {
        public float damageAmount;
        public Vector2 hitPoint;
        public AudioClip hitSfx;
    }

    public delegate void hitDelegate(HitDetails details);
    public hitDelegate executionDelegate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void triggerDelegate(HitDetails details)
    {
        executionDelegate(details);
    }

    public interface Effect
    {
        void OnEnemyHitEffect(HitDetails details);
    }
}
