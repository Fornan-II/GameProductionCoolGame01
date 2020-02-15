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

    public bool IsAlive { get { return _health > 0; } }

    //Info for differentiating between damage types
    public bool CanDie = true;
    public bool ResistProjectileDamage = false;
    public bool ResistCollisionDamage = false;
    public bool ResistExplosionDamage = false;
    public bool ResistKnockback = false;
    public bool DoDamageFlash = true;

    //Event information for when this DamageReciever "dies"
    public delegate void DamageReceiverEvent(DamageReceiver receiver, IDamageDealer dealer);
    public delegate void DamagePacketEvent(DamagePacket packet);
    public event DamageReceiverEvent OnDeath;
    public event DamagePacketEvent OnTakeDamage;

    private IDamageDealer _self;

    private void Start()
    {
        _self = GetComponent<IDamageDealer>();
    }

    // Method in charge of making this DamageReceiver have it's health decrement & receive knockback.
    // Takes in parameter damage of type DamagePacket.
    // DamagePackets are 3 parts: DamageType, DamageAmount, and KnockbackVector.
    // DamagePackets MUST define a DamageType, but defining DamageAmount and KnockbackVector are optional (will default to 1 and Vector2.zero, respectively)
    public virtual void TakeDamage(DamagePacket damage, Vector2? hitPoint = null)
    {
        //Already dead don't do anything
        if(!IsAlive)
        {
            return;
        }

        //If this DamageReciever isn't resistant to the type of damage it's taking, subtract the damage from health.
        if(!ResistCollisionDamage && damage.Type == DamageType.COLLISION || !ResistProjectileDamage && damage.Type == DamageType.PROJECTILE || !ResistExplosionDamage && damage.Type == DamageType.EXPLOSION)
        {
            _health -= damage.DamageAmount;
            if(damage.DamageAmount > 0)
            {
                OnTakeDamage?.Invoke(damage);
                Debug.Log(name + " takes " + damage.DamageAmount + " " + damage.Type + " damage from " + damage.DamageDealer);
                damage.DamageDealer?.OnDamageDealtTo(this);
            }
        }

        if(damage.DamageAmount > 0 && DamageFlashCoroutine == null && DoDamageFlash)
        {
            DamageFlashCoroutine = StartCoroutine(DamageFlash());
        }

        //If there is a rigidbody2D on this GameObject and this DamageReciever doesn't resist knockback, apply knockback from DamagePacket.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb && !ResistKnockback)
        {
            if (hitPoint.HasValue)
            {
                rb.AddForceAtPosition(damage.KnockbackVector, hitPoint.Value, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(damage.KnockbackVector, ForceMode2D.Impulse);
            }
        }

        if (!IsAlive && CanDie)
        {
            Die(damage.DamageDealer);
        }
    }

    public virtual void SetHealth(int amount)
    {
        _health = amount;
    }

    public virtual void Die(IDamageDealer killer)
    {
        _health = -1;
        killer.OnKilled(this);
        OnDeath?.Invoke(this, killer);
    }

    public SpriteRenderer UnitSpriteRenderer;
    protected Coroutine DamageFlashCoroutine;

    protected virtual void OnDestroy()
    {
        StopAllCoroutines();
    }

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

    //public void ForceStopDamageFlash()
    //{
    //    if (DamageFlashCoroutine != null)
    //        StopCoroutine(DamageFlashCoroutine);
    //}
}
