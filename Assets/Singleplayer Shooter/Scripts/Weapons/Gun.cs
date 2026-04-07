using System;
using UnityEngine;

public abstract class Gun : BaseWeapon
{
    protected float lastFireTime;

    public static event Action OnGunShoot;

    public override void Use()
    {
        if (Time.time >= lastFireTime + 1f / data.fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
            OnGunShoot?.Invoke();
        }
    }

    protected abstract void Shoot();
}
