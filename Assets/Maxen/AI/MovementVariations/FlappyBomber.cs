using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBomber : FlappyMovement
{
    public float aggroXDistance = 2.0f;
    public float aggroYDistance = 20.0f;

    public float diveBombForce = 3.0f;

    [SerializeField] protected BoopableDamageReceiver _unitDamageReciever;
    [SerializeField] protected AIUnit _unit;

    
    protected bool _primed = false;

    protected override void Start()
    {
        base.Start();
        _unitDamageReciever.OnDealDamage += OnBoop;
    }

    public override void ProcessMovement(float deltaTime)
    {
        Vector2 vectorToPlayer = PlayerScript.Instance.transform.position - transform.position;
        //Check to see if player is close horizontally and vertically... while also being underneath this enemy
        if (Mathf.Abs(vectorToPlayer.x) < aggroXDistance && Mathf.Abs(vectorToPlayer.y) < aggroYDistance && PlayerScript.Instance.transform.position.y < transform.position.y)
        {
            if (!_primed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -diveBombForce);
                _primed = true;
            }
        }
        else
        {
            _primed = false;
            base.ProcessMovement(deltaTime);
        }
    }

    protected virtual void OnBoop(DamageReceiver boopedDR)
    {
        _unit.DeathHealthReward = 0;
        _unitDamageReciever.TakeDamage(new DamagePacket(DamageType.COLLISION, _unitDamageReciever.Health));
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if(!_unit)
        {
            _unit = GetComponent<AIUnit>();
        }
        if(!_unitDamageReciever)
        {
            _unitDamageReciever = GetComponent<BoopableDamageReceiver>();
        }
    }
#endif
}
