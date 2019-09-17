using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosive : MonoBehaviour
{
    public Explosion explosionData;
    public GameObject explosionParticles;

    [SerializeField] protected DamageReceiver _damageReceiver;

    protected virtual void Start()
    {
        _damageReceiver.OnDeath += OnDeath;
    }

    protected virtual void OnDeath(DamageReceiver dr)
    {
        if(explosionParticles)
        {
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
        }

        explosionData.ExplodeAt(transform.position);
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionData.EffectiveRadius);
    }

    protected virtual void OnValidate()
    {
        if(!_damageReceiver)
        {
            _damageReceiver = GetComponent<DamageReceiver>();
        }
    }
#endif
}
