using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCharger : HorizontalMover
{
    public float aggroYDistance = 3.0f;
    public float chargeSpeed = 6.0f;
    public float chargeLerpRate = 0.3f;

    public override void ProcessMovement(float deltaTime)
    {
        //If player is close to this enemy, charge at them.
        if(Mathf.Abs(PlayerScript.Instance.transform.position.y - transform.position.y) < aggroYDistance)
        {
            Debug.Log("CHARGE");
            //Set moving direction variable to face the direction the player is in
            movingRight = PlayerScript.Instance.transform.position.x > transform.position.x;

            Vector2 newVelocity = Vector2.left + Vector2.up * (preferredYLevel - transform.localPosition.y);
            newVelocity = newVelocity.normalized * chargeSpeed;
            if (movingRight)
            {
                newVelocity.x *= -1f;
            }
            newVelocity = Vector2.Lerp(_rb.velocity, newVelocity, chargeLerpRate * Time.timeScale);
            _rb.velocity = newVelocity;
        }
        else
        {
            Debug.Log("Normal");
            base.ProcessMovement(deltaTime);
        }
    }
}
