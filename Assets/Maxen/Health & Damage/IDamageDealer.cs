using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    void OnDamageDealtTo(DamageReceiver dr);
    void OnKilled(DamageReceiver dr);
}
