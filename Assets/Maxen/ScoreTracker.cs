using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public Transform trackedTransform;

    public float HighestYReached
    {
        get;
        protected set;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(trackedTransform)
        {
            if(trackedTransform.position.y > HighestYReached)
            {
                HighestYReached = trackedTransform.position.y;
            }
        }
    }
}
