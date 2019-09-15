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
        
        if(movingRight && _rb.velocity.x < 0.01f)
        {
            movingRight = false;
        }
        else if(!movingRight && _rb.velocity.x > -0.01f)
        {
            movingRight = true;
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
