using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{



    [SerializeField] private Gun pistol;
    [SerializeField] private Gun shotgun;
    [SerializeField] private Gun sniper;
    [SerializeField] private Gun minigun;


    public enum GunType
    {
        Pistol = 0,
        Shotgun = 1,
        Sniper = 2,
        Minigun = 3
    }
    [System.Serializable]
    public class Gun
    {
        public GunType type;
        public GameObject gunObject;
        public float knockback = 0;

    }


    private void Start()
    {
        GunType type = (GunType)Random.Range(0, 3);

        if (type == GunType.Pistol)
        {
            currentGun = pistol;
        }
        else if (type == GunType.Shotgun)
        {
            currentGun = shotgun;
        }
        else if (type == GunType.Sniper)
        {
            currentGun = sniper;
        }
        else if (type == GunType.Minigun)
        {
            currentGun = minigun;
        }

        GameObject gun = Instantiate(currentGun.gunObject, transform.position, Quaternion.identity, transform);
        
    }


    [SerializeField] private Gun currentGun;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag.Contains("Player"))
        {
            Debug.Log("hess");
            PlayerScript.Instance.SetCurrentWeapon(currentGun);
        }
    }
}
