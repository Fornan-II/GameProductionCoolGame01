using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AIMoveScript : MonoBehaviour
{
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void ProcessMovement(float deltaTime)
    {
        //override in inheriting classes. This base version just doesn't move.
    }

    public virtual void DisableMovement()
    {
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0.0f;
    }

    public virtual void EnableMovement()
    {
        _rb.isKinematic = false;
    }
}