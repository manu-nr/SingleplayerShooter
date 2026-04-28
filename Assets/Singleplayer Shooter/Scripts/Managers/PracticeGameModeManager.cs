using UnityEngine;

public class PracticeGameModeManager : GameModeManager
{
    [SerializeField] private int TotalDamageables = 30;

    [SerializeField] private int _currentDamageableIndex = 0;

    private DamageableManager _damageableManager;

    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        _damageableManager = DamageableManager.Instance;
        GunShooter.OnDamageableHit += OnShootedDamageable;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GunShooter.OnDamageableHit -= OnShootedDamageable;
    }
    #endregion

    #region Base Methods
    protected override void Begin()
    {
        base.Begin();
        ResetVars();
        Debug.Log("[NRM] Game mode Begin");
        _damageableManager.SpawnDamageable();
    }

    protected override void Complete()
    {
        base.Complete();
        Debug.Log("[NRM] Game mode Complete");

        ResetVars();
    }

    #endregion

    #region Private Methods
    private void ResetVars()
    {
        _currentDamageableIndex = 0;
    }

    private void OnShootedDamageable(WeaponData data)
    {
        if (_currentGameMode == ModeType.PRACTICE_MODE)
        {
            _damageableManager.DestoryDamageable();
            _currentDamageableIndex++;

            if (_currentDamageableIndex < TotalDamageables)
                _damageableManager.SpawnDamageable();
            else
                Complete();
        }
    }
    #endregion
}
