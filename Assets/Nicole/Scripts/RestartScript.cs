using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LevelReset"); //Should be called when the player dies
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LevelReset() //When envoked, wait a few seconds before resetting
    {
        countdownText.text = "Restarting in 5 seconds.";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Restarting in 4 seconds.";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Restarting in 3 seconds.";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Restarting in 2 seconds.";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Restarting in 1 second.";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("Master Game Scene");
    }
}
