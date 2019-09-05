using UnityEngine;

public enum DamageType
{
    PROJECTILE,
    COLLISION
}

[System.Serializable]
public struct DamagePacket
{
    public DamageType Type;
    public int DamageAmount;
    public Vector2 KnockbackVector;

    public DamagePacket(DamageType type)
    {
        Type = type;
        DamageAmount = 1;
        KnockbackVector = Vector2.zero;
    }

    public DamagePacket(DamageType type, Vector2 knockback)
    {
        Type = type;
        DamageAmount = 1;
        KnockbackVector = knockback;
    }

    public DamagePacket(DamageType type, int damage)
    {
        Type = type;
        DamageAmount = damage;
        KnockbackVector = Vector2.zero;
    }

    public DamagePacket(DamageType type, int damage, Vector2 knockback)
    {
        Type = type;
        DamageAmount = damage;
        KnockbackVector = knockback;
    }
}