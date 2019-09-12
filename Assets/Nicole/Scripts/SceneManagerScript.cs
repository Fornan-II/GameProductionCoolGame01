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
