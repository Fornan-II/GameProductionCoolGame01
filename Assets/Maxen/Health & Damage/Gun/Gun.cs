using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _knockbackScalar = 0.5f;
    [SerializeField] protected float _bulletSpeed = 68.8f;
    [SerializeField] protected float _shootKnockback = 1000.0f;
    [SerializeField] protected BulletScript _bullet;
    [SerializeField] protected Transform _bulletSpawn;
    [SerializeField] protected ParticleSystem _bulletFlash;

    protected bool _equipped = false;

    public float ShootKnockback { get { return _shootKnockback; } }

    public virtual void OnEquip()
    {
        _equipped = true;
        _bulletSpawn.gameObject.SetActive(true);
    }

    public virtual void Shoot()
    {
        Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation).Initialize(_damage, _knockbackScalar, _bulletSpeed);
        _bulletFlash.Play();
        CameraFX.MainCamera.ScreenShake();
    }

    protected IEnumerator ShootXBulletsForYSeconds(int xBullets, float ySeconds)
    {
        float timeBetweenBullets = ySeconds / (float)xBullets;

        //Only apply knockback first shot - otherwise very uncontrollable
        for (int bulletsShot = 0; bulletsShot < xBullets; bulletsShot++)
        {
            Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation).Initialize(_damage, _knockbackScalar, _bulletSpeed);
            _bulletFlash.Play();
            CameraFX.MainCamera.ScreenShake();

            float timer = 0.0f;
            while (timer < timeBetweenBullets)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
