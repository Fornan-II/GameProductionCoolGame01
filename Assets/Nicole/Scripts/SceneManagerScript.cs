using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject level;
    public static Transform TriggerHeight;
    // Start is called before the first frame update
    void Start()
    {
        HeightUpdate();
        
        //Ideas: Set all of the random object items to inactive and then when this spawns have it get a list of all those objects and set them to active after the wall moves
        // Wait a second, can't find objects if they are under a parent. Maybe have a coroutine that will loda the objects after a second
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeightUpdate()
    {
        level.transform.position = new Vector3(level.transform.position.x, level.transform.position.y + (TriggerHeight.position.y +15), level.transform.position.z); //On start, shift all objects in the stage upwards so they don't spawn over the last stage
    }
}
