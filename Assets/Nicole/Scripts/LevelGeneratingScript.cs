using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratingScript : MonoBehaviour
{
    public GameObject triangleObject;

    public PrefabPool enemyObjects;

    public PrefabPool ammoObject;

    public PrefabPool triangleObjects; 

    public bool haveBigTriangle; //if a triangle spawns, should it be the bigger one

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

    IEnumerator LoadBlocks()
    {
        yield return new WaitForSeconds(.2f);
        randNum = Random.Range(1, 5); //Get a random number and spawn a different object depending on that number

        if (randNum == 1 || randNum == 4)
        {
            if(haveBigTriangle == true)
            {
                newOb = triangleObject;
            }
            else
            {
                newOb = triangleObjects.GetRandomItem(PlayerScript.Instance.transform.position.y);
            }

        }
        else if (randNum == 2)
        {
            //SHOULD BE USING CURRENT SCORE
            newOb = enemyObjects.GetRandomItem(PlayerScript.Instance.transform.position.y);
        }
        else if (randNum == 3)
        {
            newOb = ammoObject.GetRandomItem(PlayerScript.Instance.transform.position.y); ;
        }
        
        GameObject createdObject;

        if (newOb)
        {
            if (newOb.name == "Triangle2")
            {
                if (gameObject.transform.position.x > 0)
                {
                    createdObject = Instantiate(newOb, transform.position, transform.rotation);
                    createdObject.transform.eulerAngles = new Vector3(createdObject.transform.eulerAngles.x, createdObject.transform.eulerAngles.y, createdObject.transform.eulerAngles.z + 90);
                }
                else if (gameObject.transform.position.x < 0)
                {
                    createdObject = Instantiate(newOb, transform.position, transform.rotation);
                    createdObject.transform.eulerAngles = new Vector3(createdObject.transform.eulerAngles.x, createdObject.transform.eulerAngles.y, createdObject.transform.eulerAngles.z - 90);
                }
            }
            else
            {
                createdObject = Instantiate(newOb, transform.position, transform.rotation);
            }
        }
        
    }
}
