using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject bg;
    float fadingTime = 0;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        // Make bg as big as camera proyection.
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;
        float camHeight = 2.0f * camHalfHeight;

        //var frustumHeight = 2.0f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        bg.transform.localScale = new Vector3(camWidth / 10, 1, camHeight / 10);
        rend = GetComponentInChildren<Renderer>();
        rend.sortingLayerName = "OtherUI";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = Camera.main.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        fadingTime += Time.deltaTime * 0.5f;
        Debug.Log("Fading time: " + fadingTime);
        Color startColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        Color endColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        rend.material.SetColor("_Color", Color.Lerp(startColor, endColor, fadingTime));
    }
}
