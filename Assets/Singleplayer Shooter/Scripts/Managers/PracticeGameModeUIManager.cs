using System;
using TMPro;
using UnityEngine;

public class PracticeGameModeUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _remainingdamageablesCountText;
    [SerializeField] private TextMeshProUGUI _currentGun;
    [SerializeField] private TextMeshProUGUI _currentGunAmmoText;
    [SerializeField] private TextMeshProUGUI _totalAmmo;
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _timer;
    private bool _runTimer;

    #region Unity Methods
    private void Start()
    {
        GameModeManager.OnGameModeStateChange += HandleGameModeStateChange;
    }

    private void OnDestroy()
    {
        GameModeManager.OnGameModeStateChange -= HandleGameModeStateChange;
    }

    private void Update()
    {
        if(_runTimer)
        {
            _timer += Time.deltaTime;
            int seconds = Mathf.FloorToInt(_timer % 60);
            int minutes = Mathf.FloorToInt(_timer / 60);

            _timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
    #endregion

    #region Public Methods
    public void UpdateDamageablesUI(int totalDamageables, int remainingDamageables)
    {
        _remainingdamageablesCountText.SetText($"{remainingDamageables}/{totalDamageables}");
    }

    public void UpdateWeaponsUI(WeaponData data)
    {
        _currentGun.SetText("Gun: " + data.weaponName);
        _currentGunAmmoText.SetText($"Ammo: {WeaponManager.Instance.CurrentWeapon.CurrentAmmo} / {data.magazineSize}");
        _totalAmmo.SetText($"{WeaponManager.Instance.CurrentWeapon.TotalAmmo}");
    }
    #endregion

    #region Private Methods
    private void HandleGameModeStateChange(ModeType type, bool isStarted)
    {
        if(type == ModeType.PRACTICE_MODE)
        {
            if (isStarted)
                StartTimer();
            else
                StopTimer();
        }
    }

    private void StartTimer()
    {
        _timer = 0f;
        _runTimer = true;
    }

    private void StopTimer()
    {
        _runTimer = false;
    }
    #endregion
}
