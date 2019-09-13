using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : AIMoveScript
{
    public float moveSpeed = 1.0f;
    public bool movingRight = true;
    public float lerpRate = 0.7f;

    protected float preferredYLevel;
    protected Collider2D _col;

    public LayerMask movementReversingLayers;

    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        preferredYLevel = transform.localPosition.y;
    }

    public Vector2 velocityMirror;
    public override void ProcessMovement(float deltaTime)
    {
        base.ProcessMovement(deltaTime);

        velocityMirror = _rb.velocity;
        if(Mathf.Abs(_rb.velocity.sqrMagnitude) < 0.01)
        {
            movingRight = !movingRight;
        }

        //Calculate moveVelocity
        //
        Vector2 newVelocity = Vector2.left + Vector2.up * (preferredYLevel - transform.localPosition.y);
        newVelocity = newVelocity.normalized * moveSpeed;
        if(movingRight)
        {
            newVelocity.x *= -1f;
        }
        newVelocity = Vector2.Lerp(_rb.velocity, newVelocity, lerpRate * Time.timeScale);
        _rb.velocity = newVelocity;
        //
    }
}
