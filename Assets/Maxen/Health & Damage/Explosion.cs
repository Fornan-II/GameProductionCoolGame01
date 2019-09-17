﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Explosion
{
    public int BaseDamageAmount = 3;
    public float EffectiveRadius = 5.0f;
    public float BaseKnockbackStrength = 10.0f;

    public DamagePacket GetDamagePacket(Vector2 explosionPoint, Vector2 hitPoint)
    {
        DamagePacket packet = new DamagePacket(DamageType.EXPLOSION);

        Vector2 vecToHitPoint = hitPoint - explosionPoint;

        float distance = 1.0f - vecToHitPoint.magnitude / EffectiveRadius;

        float pointMagnitude = distance * distance;// 0.75f / (Mathf.PI * Mathf.Pow(distance, 3.0f));

        packet.DamageAmount = Mathf.RoundToInt(pointMagnitude * BaseDamageAmount);
        packet.KnockbackVector = vecToHitPoint.normalized * pointMagnitude * BaseKnockbackStrength;

        Debug.Log(packet);
        return packet;
    }

    public void ExplodeAt(Vector2 explosionPoint)
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(explosionPoint, EffectiveRadius);

        foreach(Collider2D col in overlappedColliders)
        {
            DamageReceiver dr = col.GetComponent<DamageReceiver>();
            if(dr)
            {
                Vector2 hitPoint = col.OverlapPoint(explosionPoint) ? (Vector2)col.transform.position : col.ClosestPoint(explosionPoint);
                dr.TakeDamage(GetDamagePacket(explosionPoint, hitPoint), hitPoint);
            }
        }
        PlayerScript.Instance?.ScreenShake();
    }
}