using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemo : MonoBehaviour
{
    bool finishedDemo = false;
    GameObject player;
    GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!finishedDemo)
        {
            finishedDemo = true;
            // Check if we collided with the player.
            if (collision.gameObject == player)
            {
                GlobalEvents geScript = gameController.GetComponent<GlobalEvents>();
                geScript.ShowDemoEndScreen();
            }
        }
        
    }
}
