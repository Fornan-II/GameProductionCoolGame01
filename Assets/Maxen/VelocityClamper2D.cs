using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityClamper2D : MonoBehaviour
{
    Rigidbody2D _rb;
    public float velocityMax = -1.0f;
    public float velocityMin = -1.0f;
    public float spinMax = -1.0f;
    public float spinMin = -1.0f;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        //Velocity
        //
        if(velocityMax >= 0.0f)
        {
            if(_rb.velocity.sqrMagnitude > velocityMax * velocityMax)
            {
                _rb.velocity = _rb.velocity.normalized * velocityMax;
            }
        }
        if (velocityMin >= 0.0f)
        {
            if (_rb.velocity.sqrMagnitude < velocityMin * velocityMin)
            {
                _rb.velocity = _rb.velocity.normalized * velocityMin;
            }
        }
        //

        //Angular Velocity
        //
        if (spinMax >= 0.0f)
        {
            if (Mathf.Abs(_rb.angularVelocity) > spinMax)
            {
                _rb.angularVelocity = _rb.angularVelocity < 0.0f ? -spinMax : spinMax;
            }
        }
        if (spinMin >= 0.0f)
        {
            if (Mathf.Abs(_rb.angularVelocity) < spinMin)
            {
                _rb.angularVelocity = _rb.angularVelocity < 0.0f ? -spinMin : spinMin;
            }
        }
        //
    }
}
