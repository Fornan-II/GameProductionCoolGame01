using UnityEngine;

public enum DamageType
{
    PROJECTILE,
    COLLISION,
    EXPLOSION
}

[System.Serializable]
public struct DamagePacket
{
    public DamageType Type;
    public int DamageAmount;
    public Vector2 KnockbackVector;
    public IDamageDealer DamageDealer;

    public DamagePacket(DamageType type, IDamageDealer damageDealer)
    {
        Type = type;
        DamageAmount = 1;
        KnockbackVector = Vector2.zero;
        DamageDealer = damageDealer;
    }

    public DamagePacket(DamageType type, Vector2 knockback, IDamageDealer damageDealer)
    {
        Type = type;
        DamageAmount = 1;
        KnockbackVector = knockback;
        DamageDealer = damageDealer;
    }

    public DamagePacket(DamageType type, int damage, IDamageDealer damageDealer)
    {
        Type = type;
        DamageAmount = damage;
        KnockbackVector = Vector2.zero;
        DamageDealer = damageDealer;
    }

    public DamagePacket(DamageType type, int damage, Vector2 knockback, IDamageDealer damageDealer)
    {
        Type = type;
        DamageAmount = damage;
        KnockbackVector = knockback;
        DamageDealer = damageDealer;
    }

    public override string ToString()
    {
        return Type + " | " + DamageAmount + " | " + KnockbackVector;
    }
}