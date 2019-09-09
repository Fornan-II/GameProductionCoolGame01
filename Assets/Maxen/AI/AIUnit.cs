using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic AI
//No movement or attack patterns.
//Other AI types inherit from this class
public class AIUnit : MonoBehaviour
{
    //Stop processing at a distance, and then at a closer distance, start processing.
    //Two vars so that player doing small movements at the cusp doesn't constantly trigger this
    //Square distance because it's better faster for comparing distance
    protected const float StartProcessUnitSquareDistance = 25.0f * 25.0f;
    protected const float StopProcessUnitSquareDistance = 30.0f * 30.0f;

    //Probably want to retrieve this from some sort of GameManager class
    public Transform playerTransform;

    public AIMoveScript movement;
    //weaponScript;

    public bool LetProcessAI = true;
    [SerializeField]
    protected bool _isProcessing = false;

    protected virtual void Start()
    {
        playerTransform = PlayerScript.Instance.transform;
        DamageReceiver dr = GetComponent<DamageReceiver>();
        if(dr)
        {
            dr.OnDeath += DefaultOnDeath;
        }
    }

    protected virtual void FixedUpdate()
    {
        if(!LetProcessAI)
        {
            return;
        }

        float sqrDistanceToPlayer = 0.0f;
        if (playerTransform)
        {
            sqrDistanceToPlayer = (transform.position - playerTransform.position).sqrMagnitude;
        }
        
        if (_isProcessing)
        {
            ProcessAI();
            if (sqrDistanceToPlayer > StopProcessUnitSquareDistance)
            {
                _isProcessing = false;
            }
        }
        else if(sqrDistanceToPlayer < StartProcessUnitSquareDistance)
        {
            _isProcessing = true;
        }
    }

    //Where to process most of the AIs behaviors. Updates on Time.fixedDeltaTime.   
    protected virtual void ProcessAI()
    {
        if(movement)
        {
            movement.ProcessMovement(Time.fixedDeltaTime);
        }
    }

    protected virtual void DefaultOnDeath(DamageReceiver dr)
    {
        LetProcessAI = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.gravityScale = 1.0f;
        }

        Destroy(gameObject, 3.0f);
    }
}
