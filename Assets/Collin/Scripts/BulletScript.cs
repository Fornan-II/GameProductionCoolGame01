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
        //Initialize();
    }

    public void Initialize(int damage, float kbScalar, float bulletSpeed)
    {
        Destroy(gameObject, timeTilDestroy);

        Damage = damage;
        KnockbackScalar = kbScalar;

        bulletRig = GetComponent<Rigidbody2D>();
        SetVelocity(bulletSpeed);
    }

    private void SetVelocity(float aVelocityMultiplier)
    {
        bulletRig.velocity = transform.right * aVelocityMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        DamageReceiver hitDR;
        if(hit.TryGetComponent(out hitDR))
        {
            hitDR.TakeDamage(new DamagePacket(DamageType.PROJECTILE, Damage, bulletRig.velocity * KnockbackScalar), hit.ClosestPoint(transform.position));
        }
    }
}
