using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingScript : MonoBehaviour
{
    public int sceneIndex; //Current scenes index number
    Transform triggerPos; //The position of the trigger

    // Start is called before the first frame update
    void Start()
    {
        triggerPos = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if(sceneIndex == 1)
        {
            SceneManager.LoadSceneAsync("TestScene2", LoadSceneMode.Additive);
            Debug.Log("Level Loaded");
            SceneManagerScript.TriggerHeight = triggerPos;
            gameObject.SetActive(false);
        }
        else if(sceneIndex == 2)
        {
            SceneManager.LoadSceneAsync("TestScene", LoadSceneMode.Additive);
            Debug.Log("Level Loaded");
            SceneManagerScript.TriggerHeight = triggerPos;
            gameObject.SetActive(false);
        }
    }
}
