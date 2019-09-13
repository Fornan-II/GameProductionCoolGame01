using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put this class on something if you want it to take damage.
public class DamageReceiver : MonoBehaviour
{
    protected static Color DamageFlashColor = Color.red;
    const float DamageFlashDuration = 0.1f;

    //If you want to adjust health externally, use TakeDamage()
    [SerializeField]protected int _health = 1;
    public int Health
    {
        get { return _health; }
    }

    //Info for differentiating between damage types
    public bool CanDie = true;
    public bool ResistProjectileDamage = false;
    public bool ResistCollisionDamage = false;
    public bool ResistKnockback = false;

    //Event information for when this DamageReciever "dies"
    public delegate void DamageReceiverEvent(DamageReceiver dr);
    public delegate void DamagePacketEvent(DamagePacket packet);
    public event DamageReceiverEvent OnDeath;
    public event DamagePacketEvent OnTakeDamage;

    // Method in charge of making this DamageReceiver have it's health decrement & receive knockback.
    // Takes in parameter damage of type DamagePacket.
    // DamagePackets are 3 parts: DamageType, DamageAmount, and KnockbackVector.
    // DamagePackets MUST define a DamageType, but defining DamageAmount and KnockbackVector are optional (will default to 1 and Vector2.zero, respectively)
    public virtual void TakeDamage(DamagePacket damage)
    {
        //If this DamageReciever isn't resistant to the type of damage it's taking, subtract the damage from health.
        if(!ResistCollisionDamage && damage.Type == DamageType.COLLISION || !ResistProjectileDamage && damage.Type == DamageType.PROJECTILE)
        {
            _health -= damage.DamageAmount;
            if(damage.DamageAmount > 0)
            {
                OnTakeDamage?.Invoke(damage);
            }
        }

        if(damage.DamageAmount > 0 && DamageFlashCoroutine == null)
        {
            DamageFlashCoroutine = StartCoroutine(DamageFlash());
        }

        //If there is a rigidbody2D on this GameObject and this DamageReciever doesn't resist knockback, apply knockback from DamagePacket.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb && !ResistKnockback)
        {
            rb.AddForce(damage.KnockbackVector, ForceMode2D.Impulse);
        }

        if (_health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        if (CanDie)
        {
            ResistCollisionDamage = true;
            ResistProjectileDamage = true;
            _health = -1;

            OnDeath?.Invoke(this);
        }
    }

    public SpriteRenderer UnitSpriteRenderer;
    protected Coroutine DamageFlashCoroutine;

    protected virtual IEnumerator DamageFlash()
    {
        if(UnitSpriteRenderer)
        {
            Color originalColor = UnitSpriteRenderer.color;
            UnitSpriteRenderer.color = DamageFlashColor;
            yield return new WaitForSeconds(DamageFlashDuration);
            UnitSpriteRenderer.color = originalColor;
        }

        DamageFlashCoroutine = null;
    }
}
