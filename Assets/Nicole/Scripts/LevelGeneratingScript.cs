using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratingScript : MonoBehaviour
{
    public GameObject triangleObject;

    public PrefabPool enemyObjects;

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

    IEnumerator LoadBlocks()
    {
        yield return new WaitForSeconds(.5f);
        randNum = Random.Range(1, 5); //Get a random number and spawn a different object depending on that number

        if (randNum == 1 || randNum == 4)
        {
            newOb = triangleObject;
        }
        else if (randNum == 2)
        {
            //SHOULD BE USING CURRENT SCORE
            newOb = enemyObjects.GetRandomItem(PlayerScript.Instance.transform.position.y);
        }
        else if (randNum == 3)
        {
            newOb = ammoObject;
        }

        GameObject createdObject = Instantiate(newOb, transform.position, transform.rotation);

        createdObject.transform.parent = gameObject.transform;
    }
}
