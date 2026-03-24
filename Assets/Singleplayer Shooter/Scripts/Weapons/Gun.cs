using UnityEngine;

public abstract class Gun : BaseWeapon
{
    protected float lastFireTime;

    public override void Use()
    {
        if (Time.time >= lastFireTime + 1f / data.fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    protected abstract void Shoot();
}
