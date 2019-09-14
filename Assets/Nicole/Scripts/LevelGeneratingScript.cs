using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratingScript : MonoBehaviour
{
    public GameObject triangleObject;

    public GameObject enemyObject;

    public GameObject ammoObject;

    private int randNum;

    private GameObject newOb; //The newly spawned object
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadBlocks");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObjects()
    {
        randNum = Random.Range(1, 5); //Get a random number and spawn a different object depending on that number

        if (randNum == 1 || randNum == 4)
        {
            newOb = triangleObject;
            //Instantiate(triangleObject, transform.position, transform.rotation);
        }
        else if (randNum == 2)
        {
            newOb = enemyObject;
            //Instantiate(enemyObject, transform.position, transform.rotation);
        }
        else if (randNum == 3)
        {
            newOb = ammoObject;
            //Instantiate(ammoObject, transform.position, transform.rotation);
        }

        Instantiate(newOb, transform.position, transform.rotation);

        newOb.transform.parent = gameObject.transform;
    }

    IEnumerator LoadBlocks()
    {
        yield return new WaitForSeconds(.5f);
        randNum = Random.Range(1, 5); //Get a random number and spawn a different object depending on that number

        if (randNum == 1 || randNum == 4)
        {
            newOb = triangleObject;
            //Instantiate(triangleObject, transform.position, transform.rotation);
        }
        else if (randNum == 2)
        {
            newOb = enemyObject;
            //Instantiate(enemyObject, transform.position, transform.rotation);
        }
        else if (randNum == 3)
        {
            newOb = ammoObject;
            //Instantiate(ammoObject, transform.position, transform.rotation);
        }

        Instantiate(newOb, transform.position, transform.rotation);

        newOb.transform.parent = gameObject.transform;
    }
}
