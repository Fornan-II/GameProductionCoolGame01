using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AIMoveScript : MonoBehaviour
{
    protected Rigidbody2D _rb;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void ProcessMovement(float deltaTime)
    {
        //override in inheriting classes. This base version just doesn't move.
    }
}