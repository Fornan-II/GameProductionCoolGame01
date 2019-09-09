using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private float timeTilDestroy = 0;

    private void Start()
    {
        StartCoroutine(DestroyTimer(timeTilDestroy));
        SetVelocity(bulletSpeed);
    }

    private void SetVelocity(float aVelocityMultiplier)
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * aVelocityMultiplier;
    }

    private IEnumerator DestroyTimer(float aTimeTilDestroy)
    {
        yield return new WaitForSecondsRealtime(aTimeTilDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.tag == "Enemy")
        {
            //Enemy.Destroy()
        }
    }
}
