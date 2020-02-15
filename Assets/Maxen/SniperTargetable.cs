using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTargetable : MonoBehaviour
{
    [SerializeField] protected bool _isTargetable = true;
    public bool IsTargetable { get { return _isTargetable; } }

    private void Start()
    {
        if(TryGetComponent(out DamageReceiver dr))
        {
            dr.OnDeath += (receiver, killer) => { _isTargetable = false; };
        }
    }
}
