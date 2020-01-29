using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericBurstControl : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ps.IsAlive())
            Destroy(gameObject);
    }
}
