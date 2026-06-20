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
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private float _timer;
    private bool _runTimer;

    private float _highScoreTime;
    private bool _isHighScoreSet;

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
            {
                
                SetHighScoreText();
                StartTimer();
            }
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
        SaveHighScore();
    }

    private void SetHighScoreText()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            _isHighScoreSet = true;
            _highScoreTime = PlayerPrefs.GetFloat("HighScore");

            int seconds = Mathf.FloorToInt(_highScoreTime % 60);
            int minutes = Mathf.FloorToInt(_highScoreTime / 60);

            _highScoreText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
        else
        {
            _isHighScoreSet = false;
            _highScoreText.SetText("None");
        }
    }

    private void SaveHighScore()
    {
        bool saveHighScore = false;

        if (!_isHighScoreSet)
        {
            _highScoreTime = _timer;
            saveHighScore = true;
        }
        else
        {
            if (_timer < _highScoreTime)
            {
                _highScoreTime = _timer;
                saveHighScore = true;
            }
        }

        if (saveHighScore)
            PlayerPrefs.SetFloat("HighScore", _highScoreTime);
    }
    #endregion
}
