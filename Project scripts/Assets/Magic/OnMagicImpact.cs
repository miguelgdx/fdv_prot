using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMagicImpact : MonoBehaviour
{
    public class ImpactDetails
    {
        public MagicBase.MagicName skillName;
        public Vector2 hitPoint;
    }
    public delegate void MyDelegate(ImpactDetails details);
    public MyDelegate effectDelegate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerDelegate(ImpactDetails details)
    {
        effectDelegate(details);
    }

    public interface Effect
    {
        void OnMagicImpactEffect(ImpactDetails details);
    }
}
