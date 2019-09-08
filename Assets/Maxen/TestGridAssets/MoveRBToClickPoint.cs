using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRBToClickPoint : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector3 objectLastPos;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _rb.isKinematic = false;

            _rb.velocity = (transform.position - objectLastPos) / Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0.0f;
            _rb.isKinematic = true;
            
            float oldZPos = transform.position.z;
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = oldZPos;
            objectLastPos = transform.position;
            transform.position = newPos;
        }
    }
}
