using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFadeScript : MonoBehaviour
{
    private Image screen;
    bool colorGo = false;
    // Start is called before the first frame update
    void Start()
    {
        screen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (screen.color == Color.black)
        {
            Debug.Log("Black");
            SceneManager.LoadScene(1);
        }
        if (colorGo)
        {
            screen.color = Color.Lerp(screen.color, Color.black, (Time.deltaTime * 15));
            
        }
        
    }

    public void FadeScreen()
    {
        colorGo = true;
    }
}
