﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) //If any button is pressed, restart
        {
            if(timer > 0)
            {

            }
            else
            {
                //Pop up a menu that says "Play again" or "Return to main menu" - play again is default option
                SceneManager.LoadSceneAsync(0);
            }
            
        }
        timer -= Time.deltaTime;
    }
    
}
