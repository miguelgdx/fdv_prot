using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingEffect : MonoBehaviour
{
    public List<GameObject> poolEffects;
    public GameObject blueCast;
    public GameObject redCast;
    private float castingEffectOpacity = 0f;
    private bool active = false;
    private SpriteRenderer activeSprite;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate and store effects in pool.
        poolEffects = new List<GameObject>();

        // 0 = Blue casting
        // 1 = Red casting.

        GameObject obj = (GameObject)Instantiate(blueCast, transform);
        obj.transform.localPosition = new Vector2(0, 0);
        obj.SetActive(false);
        poolEffects.Add(obj);
        obj = (GameObject)Instantiate(redCast, transform);
        obj.transform.localPosition = new Vector2(0, 0);
        obj.SetActive(false);
        poolEffects.Add(obj);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (active == true)
        {
            if (castingEffectOpacity < 1f)
            {
                castingEffectOpacity += Time.deltaTime;
                activeSprite.color = new Color(1f, 1f, 1f, castingEffectOpacity);
            }
        }
    }

    public void showCast(int index)
    {
        hideAllCast();
        SpriteRenderer sr = poolEffects[index].GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        poolEffects[index].SetActive(true);
        active = true;
        activeSprite = sr;
    }
    public void hideAllCast()
    {
        foreach (GameObject obj in poolEffects)
        {
            obj.SetActive(false);
        }
        active = false;
        castingEffectOpacity = 0f;
    }
    public void setLayerOrder(int order)
    {
        foreach(GameObject obj in poolEffects)
        {
            SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = order;
        }
    }
}
