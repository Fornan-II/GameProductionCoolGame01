using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoopableDamageReceiver : DamageReceiver
{
    [Range(0.0f, 360.0f)]
    public float DamageRecievingAngle = 180.0f;

    public float knockbackStrength = 3.0f;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("CanBoop"))
        {
            return;
        }

        Vector2 contactPoint = collision.GetContact(0).point;
        Vector2 vectorToContact = contactPoint - (Vector2)transform.position;

        Vector2 knockbackVector = collision.GetContact(0).normal;
        knockbackVector = knockbackVector * knockbackStrength;

        if (Vector2.Angle(transform.up, vectorToContact) < DamageRecievingAngle * 0.5f)
        {
            //This DR is taking damage
            collision.rigidbody?.AddForce(-knockbackVector, ForceMode2D.Impulse);
            TakeDamage(new DamagePacket(DamageType.COLLISION, knockbackVector));
        }
        else
        {
            //Other DR is taking damage
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb?.AddForce(knockbackVector, ForceMode2D.Impulse);
            DamageReceiver otherDR = collision.transform.GetComponent<DamageReceiver>();
            otherDR?.TakeDamage(new DamagePacket(DamageType.COLLISION, -knockbackVector));
        }
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        float halfAngle = DamageRecievingAngle * 0.5f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(halfAngle, Vector3.forward);
        Quaternion rightRayRotation = Quaternion.AngleAxis(-halfAngle, Vector3.forward);
        Vector3 leftRayDirection = leftRayRotation * transform.up;
        Vector3 rightRayDirection = rightRayRotation * transform.up;

        //Draw area where DR recieves damage
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.forward, rightRayDirection, DamageRecievingAngle, 1.0f);
        //Draw area where DR deals damage
        UnityEditor.Handles.color = Color.green;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftRayDirection * 1.0f);
        Gizmos.DrawRay(transform.position, rightRayDirection * 1.0f);
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.forward, rightRayDirection, DamageRecievingAngle - 360, 1.0f);
    }
#endif
}
