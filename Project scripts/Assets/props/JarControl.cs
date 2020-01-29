using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarControl : MonoBehaviour, OnMagicImpact.Effect
{
    OnMagicImpact magicImpactScript;
    public GameObject brokenJarProp;
    ItemSpawner spawnScript;
    // Start is called before the first frame update
    void Start()
    {
        magicImpactScript = GetComponent<OnMagicImpact>();
        if (magicImpactScript != null)
            magicImpactScript.effectDelegate = OnMagicImpactEffect;

        spawnScript = GetComponent<ItemSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMagicImpactEffect(OnMagicImpact.ImpactDetails details)
    {
        Destroy(gameObject);
        spawnScript.SpawnItem();
        GameObject go = (GameObject)GameObject.Instantiate(brokenJarProp);
        // Hacer que use el mismo sorting order que el item actual.
        Renderer re = go.GetComponent<Renderer>();
        re.sortingOrder = gameObject.GetComponent<Renderer>().sortingOrder;
        go.transform.position = transform.position;
        Vector2 posCorrection = new Vector2(-0.039f + transform.position.x, -0.224f + transform.position.y);
        go.transform.position = posCorrection;
    }
}
