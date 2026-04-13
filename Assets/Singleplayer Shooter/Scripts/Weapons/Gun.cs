using System;
using UnityEngine;

public abstract class Gun : BaseWeapon
{
    [SerializeField] protected ParticleSystem _muzzleFlashParticle;

    protected float lastFireTime;

    public static event Action OnGunShoot;

    public override void Use()
    {
        if (Time.time >= lastFireTime + 1f / data.fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
            OnGunShoot?.Invoke();
            _muzzleFlashParticle.Play();
        }
    }

    protected abstract void Shoot();
}
