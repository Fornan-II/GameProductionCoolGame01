using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic AI
//No movement or attack patterns.
//Other AI types inherit from this class
public class AIUnit : MonoBehaviour
{
    //Stop processing at a distance, and then at a closer distance, start processing.
    //Two vars so that player doing small movements at the cusp doesn't constantly trigger this
    //Square distance because it's better faster for comparing distance
    protected const float StartProcessUnitSquareDistance = 25.0f * 25.0f;
    protected const float StopProcessUnitSquareDistance = 30.0f * 30.0f;

    //Probably want to retrieve this from some sort of GameManager class
    public Transform playerTransform;

    public bool LetProcessAI = false;

    protected virtual void FixedUpdate()
    {
        float sqrDistanceToPlayer = 0.0f;
        if (playerTransform)
        {
            sqrDistanceToPlayer = (transform.position - playerTransform.position).sqrMagnitude;
        }

        if (LetProcessAI)
        {
            ProcessAI();
            if (sqrDistanceToPlayer > StopProcessUnitSquareDistance)
            {
                LetProcessAI = false;
            }
        }
        else if(sqrDistanceToPlayer < StartProcessUnitSquareDistance)
        {
            LetProcessAI = true;
        }
    }

    //Where to process most of the AIs behaviors. Updates on Time.deltaTime.   
    protected virtual void ProcessAI()
    {
        //Overriden in inheriting classes.
    }
}
