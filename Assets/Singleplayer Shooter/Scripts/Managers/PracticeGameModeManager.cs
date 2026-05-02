using System;
using TMPro;
using UnityEngine;

public class PracticeGameModeManager : GameModeManager
{
    [Header("Damageables")]
    [SerializeField] private int _totalDamageables = 30;
    [SerializeField] private int _currentDamageableIndex = 0;

    //[Header("UI")]
    //[SerializeField] private GameObject _practiceGameModeUI;
    //[SerializeField] private TextMeshProUGUI _remainingdamageablesCountText;

    private DamageableManager _damageableManager;

    public static event Action<int, int> UpdateDamageablesCountUI;

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
        _damageableManager.SpawnDamageable();
        UpdateDamageablesCountUI?.Invoke(_totalDamageables, _totalDamageables - _currentDamageableIndex);
    }

    protected override void Complete()
    {
        base.Complete();
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

            if (_currentDamageableIndex < _totalDamageables)
            {
                _damageableManager.SpawnDamageable();
                UpdateDamageablesCountUI?.Invoke(_totalDamageables, _totalDamageables - _currentDamageableIndex);
            }
            else
                Complete();
        }
    }

    //private void SetDamageablesCountText()
    //{
    //    _remainingdamageablesCountText.SetText($"{_totalDamageables - _currentDamageableIndex}/{_totalDamageables}");
    //}
    #endregion
}
