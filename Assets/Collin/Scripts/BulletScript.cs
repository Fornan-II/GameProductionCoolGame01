using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    [SerializeField] private float timeTilDestroy = 0;

    private float bulletSpeed = 0;
    private int Damage = 3;
    private float KnockbackScalar = 1.0f;
    private IDamageDealer bulletSource;

    [SerializeField]private Rigidbody2D bulletRig;

    [SerializeField] private Renderer _renderer;
    private float _timeSinceVisible = 0.0f;

    private void Update()
    {
        if(_renderer.isVisible)
        {
            _timeSinceVisible = 0.0f;
        }
        else
        {
            if (_timeSinceVisible >= timeTilDestroy)
                Destroy(gameObject);

            _timeSinceVisible += Time.deltaTime;
        }
    }

    public void Initialize(int damage, float kbScalar, float bulletSpeed, IDamageDealer source)
    {
        Damage = damage;
        KnockbackScalar = kbScalar;
        bulletSource = source;

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
            hitDR.TakeDamage(new DamagePacket(DamageType.PROJECTILE, Damage, bulletRig.velocity * KnockbackScalar, bulletSource), hit.ClosestPoint(transform.position));
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!_renderer)
            _renderer = GetComponent<Renderer>();
    }
#endif
}
