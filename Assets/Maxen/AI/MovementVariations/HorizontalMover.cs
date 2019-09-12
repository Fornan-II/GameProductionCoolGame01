using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : AIMoveScript
{
    public float moveSpeed = 1.0f;
    public bool movingRight = true;
    public float lerpRate = 0.7f;

    protected float preferredYLevel;

    public LayerMask movementReversingLayers;

    protected virtual void Start()
    {
        preferredYLevel = transform.localPosition.y;
    }

    public override void ProcessMovement(float deltaTime)
    {
        base.ProcessMovement(deltaTime);
        
        //Calculate moveVelocity
        Vector2 newVelocity = Vector2.left + Vector2.up * (preferredYLevel - transform.localPosition.y);
        newVelocity = newVelocity.normalized * moveSpeed;
        if(movingRight)
        {
            newVelocity.x *= -1f;
        }
        newVelocity = Vector2.Lerp(_rb.velocity, newVelocity, lerpRate * Time.timeScale);
        _rb.velocity = newVelocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
        if (movementReversingLayers == (movementReversingLayers | (1 << collision.gameObject.layer)))
        {
            movingRight = !movingRight;
        }
    }
}
