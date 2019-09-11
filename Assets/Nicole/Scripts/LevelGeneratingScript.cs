using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratingScript : MonoBehaviour
{
    public GameObject triangleObject;

    public GameObject enemyObject;

    public GameObject ammoObject;

    private int randNum;
    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(1, 5);

        if(randNum == 1 || randNum == 4)
        {
            Instantiate(triangleObject, transform.position, transform.rotation);
        }
        else if(randNum == 2)
        {
            Instantiate(enemyObject, transform.position, transform.rotation);
        }
        else if(randNum == 3)
        {
            Instantiate(ammoObject, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
