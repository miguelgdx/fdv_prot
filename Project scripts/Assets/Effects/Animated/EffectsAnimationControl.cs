using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectsAnimationControl : MonoBehaviour
{
    Animator animator;
    public float state = 0;
    public List<UnityEvent> onAnimationEnd;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (state >= 1.0f)
            foreach (UnityEvent ev in onAnimationEnd)
                ev.Invoke();
    }
}
