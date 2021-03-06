﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic AI
//No movement or attack patterns.
//Other AI types inherit from this class
public class AIUnit : MonoBehaviour, IDamageDealer
{
    //Stop processing at a distance, and then at a closer distance, start processing.
    //Two vars so that player doing small movements at the cusp doesn't constantly trigger this
    //Square distance because it's better faster for comparing distance
    protected const float StartProcessUnitSquareDistance = 25.0f * 25.0f;
    protected const float StopProcessUnitSquareDistance = 30.0f * 30.0f;

    //Probably want to retrieve this from some sort of GameManager class

    public AIMoveScript movement;
    //weaponScript;

    public bool LetProcessAI = true;
    [SerializeField] protected bool _isProcessing = false;
    [SerializeField] protected float _deathDestroyDelay = 3.0f;

    public int DeathHealthReward = 3;

    protected virtual void Start()
    {
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
        if (PlayerScript.Instance)
        {
            sqrDistanceToPlayer = (transform.position - PlayerScript.Instance.transform.position).sqrMagnitude;
        }
        
        if (_isProcessing)
        {
            ProcessAI();
            if (sqrDistanceToPlayer > StopProcessUnitSquareDistance)
            {
                _isProcessing = false;
                movement.DisableMovement();
            }
        }
        else if(sqrDistanceToPlayer < StartProcessUnitSquareDistance)
        {
            _isProcessing = true;
            movement.EnableMovement();
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

    protected virtual void DefaultOnDeath(DamageReceiver dr, IDamageDealer killer)
    {
        LetProcessAI = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.gravityScale = 1.0f;
        }
        rb.freezeRotation = false;

        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(gameObject, _deathDestroyDelay);
    }

    public virtual void OnDamageDealtTo(DamageReceiver dr)
    {
        
    }

    public virtual void OnKilled(DamageReceiver dr)
    {
        
    }
}
