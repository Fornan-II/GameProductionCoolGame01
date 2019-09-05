using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put this class on something if you want it to take damage.
public class DamageReceiver : MonoBehaviour
{
    //If you want to adjust health externally, use TakeDamage()
    protected int _health = 1;
    public int Health
    {
        get { return _health; }
    }

    //Info for differentiating between damage types
    public bool ResistProjectileDamage = false;
    public bool ResistCollisionDamage = false;
    public bool ResistKnockback = false;

    //Event information for when this DamageReciever "dies"
    public delegate void DeathEvent(DamageReceiver dr);
    public event DeathEvent OnDeath;

    public void TakeDamage(DamagePacket damage)
    {
        //If this DamageReciever isn't resistant to the type of damage it's taking, subtract the damage from health.
        if(!ResistCollisionDamage && damage.Type == DamageType.COLLISION || !ResistProjectileDamage && damage.Type == DamageType.PROJECTILE)
        {
            _health -= damage.DamageAmount;

            //If there is a rigidbody2D on this GameObject and this DamageReciever doesn't resist knockback, apply knockback from DamagePacket.
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if(rb && !ResistKnockback)
            {
                rb.AddForce(damage.KnockbackVector);
            }
        }

        if(_health < 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        ResistCollisionDamage = true;
        ResistProjectileDamage = true;
        _health = -1;

        OnDeath?.Invoke(this);
    }
}
