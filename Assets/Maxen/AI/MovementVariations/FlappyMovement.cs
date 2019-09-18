using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMovement : HorizontalMover
{
    public float yFlapForce = 5.0f;
    public float yLevelLeniency = 5.0f;

    protected bool _isFlapping = false;

    public override void ProcessMovement(float deltaTime)
    {
        if (movingRight && _rb.velocity.x < 0.01f)
        {
            movingRight = false;
        }
        else if (!movingRight && _rb.velocity.x > -0.01f)
        {
            movingRight = true;
        }

        //Calculate moveVelocity
        //
        Vector2 newVelocity = new Vector2(moveSpeed, _rb.velocity.y);
        if (!movingRight)
        {
            newVelocity.x *= -1f;
        }
        newVelocity.x = Mathf.Lerp(_rb.velocity.x, newVelocity.x, lerpRate * Time.timeScale);

        //Figure out if should be flapping upwards or not
        if(_isFlapping)
        {
            if(transform.position.y < preferredYLevel + yLevelLeniency)
            {
                newVelocity.y += yFlapForce * deltaTime * _rb.mass;
            }
            else
            {
                _isFlapping = false;
            }
        }
        else if(transform.position.y < preferredYLevel - yLevelLeniency)
        {
            _isFlapping = true;
        }

        _rb.velocity = newVelocity;
        //
    }
}
