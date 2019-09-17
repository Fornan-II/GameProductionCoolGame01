using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealth : MonoBehaviour
{
    [SerializeField] protected DamageReceiver _trackedDR;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    public Sprite[] spriteIndicators;

    protected virtual void Start()
    {
        _trackedDR.OnTakeDamage += OnDRTakeDamage;
    }

    protected virtual void OnDRTakeDamage(DamagePacket damage)
    {
        int spriteIndex = Mathf.Clamp(_trackedDR.Health, 0, spriteIndicators.Length - 1);

        _spriteRenderer.sprite = spriteIndicators[spriteIndex];
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
