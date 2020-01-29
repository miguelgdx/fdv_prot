using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatus
    {
        public enum StatusType
        {
            idle, bouncing, chasing, attacking, dying
        }

        public StatusType status;
    }

    public EnemyStatus stats;
    public float bounceSpeed = 200;
    public float bounceTimeLength = 1;
    public float bounceTimeStart = 0;
    public float movSpeed = 5;
    public float accelerationFactor = 20;
    public Vector3 currentVelocity = Vector3.zero;
    // Start is called before the first frame update
    public virtual void Start()
    {
        stats = new EnemyStatus();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (stats.status == EnemyStatus.StatusType.bouncing)
        {
            if ((Time.time - bounceTimeStart) > bounceTimeLength)
            {
                stats.status = EnemyStatus.StatusType.idle;
            }
        }
    }
}
