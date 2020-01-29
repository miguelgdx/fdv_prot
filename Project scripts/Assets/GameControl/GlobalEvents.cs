using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameOverUI;
    public GameObject demoEndScreen;
    public GameObject demoEndUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showGameOverScreen()
    {
        gameOverScreen = Instantiate(gameOverScreen, Camera.main.transform);
        gameOverUI.SetActive(true);

    }

    public void ShowDemoEndScreen()
    {
        demoEndScreen = Instantiate(demoEndScreen, Camera.main.transform);
        demoEndUI.SetActive(true);
    }
}
