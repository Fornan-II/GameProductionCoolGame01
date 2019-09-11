using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    //Variables
   // public Transform playerMovement;
    //public Text playerScore;
    public GameObject startPosition;
    public GameObject currentPosition;
    int scoreDistance = 0;
    public float distance;
    public Text currentScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(currentPosition.transform.position, startPosition.transform.position);
        //  playerScore.text = playerMovement.position.ToString();
        currentScore.text = "Current Score: " + scoreDistance.ToString();
        
        GetDistance();
    }

    void GetDistance()
    {
        if (distance > scoreDistance)
        {
            scoreDistance = (int)distance;
        }
    }
}
