using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControl : MonoBehaviour
{
    Renderer rend;
    public float scrollSpeed = 1F;
    private Camera mainCamera;
    public float parallaxLerpFactor = 0.05f;
    public float counter = 0;
    Vector2 bgVector = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        updateParallax(true);
        followCamera(true);
    }

    void followCamera(bool xOnly)
    {
        Camera mainCamera = Camera.main;
        if (xOnly)
            transform.position = new Vector2(mainCamera.transform.position.x, transform.position.y);
        else
            transform.position = mainCamera.transform.position;
    }

    private void updateParallax(bool xOnly)
    {
        Vector2 bgVector = Vector2.zero;
        if (xOnly)
            bgVector = new Vector2(mainCamera.transform.position.x, 0);
        else
            bgVector = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);

        for (int i = 0; i < rend.materials.Length; i++)
        {
            float skyModif = 1.0f;
            Material m = rend.materials[i];
            if (i == 0)
                skyModif = 0.1f;
            else
                skyModif = 1.0f;

            // Multiplay by the material index (this will make farther materials move slower)
            // If we have a sky, that material should move really slow, so we have an extra check for it.

            if ((positionToTextureOffset(bgVector * (i + 1)) != (m.GetTextureOffset("_MainTex"))))
            {
                Vector2 lerp = Vector2.Lerp(m.GetTextureOffset("_MainTex"), positionToTextureOffset(bgVector * (i + 1)), parallaxLerpFactor);
                m.SetTextureOffset("_MainTex", (lerp * skyModif));
            }


        }
    }

    private void oldUpdateParallax()
    {
        Vector2 bgVector = new Vector2(mainCamera.transform.position.x, 0);
        for (int i = 0; i < rend.materials.Length; i++)
        {
            float skyModif = 1.0f;
            Material m = rend.materials[i];
            if (i == 0)
                skyModif = 0.1f;
            else
                skyModif = 1.0f;

            m.SetTextureOffset("_MainTex", (bgVector * scrollSpeed * 0.0005f * (i + 1) * skyModif));
        }
    }

    private void updateParallaxCont()
    {
        
        bgVector.x += 0.05f * Time.deltaTime;
        for (int i = 0; i < rend.materials.Length; i++)
        {
            float skyModif = 1.0f;
            Material m = rend.materials[i];
            if (i == 0)
                skyModif = 0.1f;
            else
                skyModif = 1.0f;

            m.SetTextureOffset("_MainTex", (bgVector * (i + 1) * skyModif));
        }
    }

    private Vector2 positionToTextureOffset(Vector2 pos)
    {
        Vector2 result = new Vector2(pos.x * scrollSpeed * 0.0005f, pos.y * scrollSpeed * 0.0005f);
        //Vector2 result = new Vector2(pos.x, pos.y);
        return result;
    }
}