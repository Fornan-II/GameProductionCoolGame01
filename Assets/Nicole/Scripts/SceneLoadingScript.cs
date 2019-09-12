using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingScript : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync("ProceduralLevel", LoadSceneMode.Additive);
        SceneManagerScript.TriggerHeight = triggerPos;
        gameObject.SetActive(false);
    }
}
