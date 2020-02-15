using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] protected float _bulletSpread = 30.0f;

    public override void Shoot(IDamageDealer shooter)
    {
        Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation * Quaternion.Euler(0, 0, _bulletSpread / 2.0f)).Initialize(_damage, _knockbackScalar, _bulletSpeed, shooter);
        Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation * Quaternion.Euler(0, 0, -_bulletSpread / 2.0f)).Initialize(_damage, _knockbackScalar, _bulletSpeed, shooter);
        //Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation).Initialize(_damage, _knockbackScalar, _bulletSpeed, shooter);
        base.Shoot(shooter);
    }
}
