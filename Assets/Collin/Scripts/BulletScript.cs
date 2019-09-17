using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private float timeTilDestroy = 0;
    [SerializeField] private int Damage = 3;
    [SerializeField] private float KnockbackScalar = 1.0f;

    [SerializeField]private Rigidbody2D bulletRig;

    private void Start()
    {
        StartCoroutine(DestroyTimer(timeTilDestroy));
        SetVelocity(bulletSpeed);

        bulletRig = GetComponent<Rigidbody2D>();
    }

    private void SetVelocity(float aVelocityMultiplier)
    {
        bulletRig.velocity = transform.right * aVelocityMultiplier;
    }

    private IEnumerator DestroyTimer(float aTimeTilDestroy)
    {
        yield return new WaitForSecondsRealtime(aTimeTilDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        DamageReceiver hitDR = hit.GetComponent<DamageReceiver>();
        hitDR?.TakeDamage(new DamagePacket(DamageType.PROJECTILE, Damage, bulletRig.velocity * KnockbackScalar), hit.ClosestPoint(transform.position));
    }
}
