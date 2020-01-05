using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public PrefabPool gunPool;

    private void Start()
    {
        GameObject gunToSpawn = gunPool.GetRandomItem(PlayerScript.Instance.transform.position.y);

        GameObject gun = Instantiate(gunToSpawn, transform);
        currentGun = gun.GetComponent<Gun>();
    }


    [SerializeField] private Gun currentGun;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag.Contains("Player"))
        {
            //Debug.Log("hess");
            PlayerScript.Instance.SetCurrentWeapon(currentGun);
            Destroy(gameObject);
        }
    }
}
