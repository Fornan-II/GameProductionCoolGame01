using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealth : MonoBehaviour
{
    [SerializeField] protected DamageReceiver _trackedDR;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    public bool DestroyAtZeroHealth = true;
    public Sprite[] spriteIndicators;

    protected virtual void Start()
    {
        _trackedDR.OnTakeDamage += OnDRTakeDamage;
        OnDRTakeDamage(new DamagePacket());
    }

    protected virtual void OnDRTakeDamage(DamagePacket damage)
    {
        int spriteIndex = Mathf.Clamp(_trackedDR.Health, 0, spriteIndicators.Length - 1);

        if (DestroyAtZeroHealth && spriteIndex == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _spriteRenderer.sprite = spriteIndicators[spriteIndex];
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(!_trackedDR)
        {
            _trackedDR = GetComponent<DamageReceiver>();
        }

        if(!_spriteRenderer)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
#endif
}
