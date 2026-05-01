using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : BaseWeapon
{
    [SerializeField] protected ParticleSystem _muzzleFlashParticle;
    [SerializeField] protected Animation _reloadAnimation;
    [SerializeField] protected int _currentAmmo;
    [SerializeField] protected int _totalAmmo;

    private Coroutine _reloadCoroutine;
    private bool _isReloading;

    protected float lastFireTime;

    public int CurrentAmmo => _currentAmmo;

    public static event Action<WeaponData> OnGunShoot;

    private void Start()
    {
        _totalAmmo = data.maxAmmo;
        _currentAmmo = data.magazineSize;
    }

    public override void Use()
    {
        if (CanShoot())
        {
            if (Time.time >= lastFireTime + 1f / data.fireRate)
            {
                Shoot();
                lastFireTime = Time.time;
                OnGunShoot?.Invoke(data);
                _muzzleFlashParticle.Play();
                _currentAmmo--;
            }
        }
    }

    public override void Reload()
    {
        if (_totalAmmo > 0)
        {
            _isReloading = true;
            _reloadAnimation.Play();
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());
        }
    }

    protected abstract void Shoot();

    private bool CanShoot()
    {
        return !_isReloading && _currentAmmo > 0;
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitUntil(() => !_reloadAnimation.isPlaying);
        _isReloading = false;

        int ammoNeeded = data.magazineSize - _currentAmmo;

        if (ammoNeeded <= _totalAmmo)
        {
            _totalAmmo -= ammoNeeded;
            _currentAmmo = data.magazineSize;
        }
        else
        {
            _currentAmmo += _totalAmmo;
            _totalAmmo = 0;
        }
    }
}
