using System;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PracticeGameModeUIManager _practiceGameUI;

    private ModeType _currentGameMode;


    private void Start()
    {
        GameModeManager.OnGameModeStateChange += HandleGameModeStateChange;
        PracticeGameModeManager.UpdateDamageablesCountUI += UpdateDamageablesCountUI;
        WeaponManager.OnGunChange += UpdateGunUI;
        Gun.OnGunShoot += UpdateGunUI;

        DisableAllUI();
    }

    private void OnDestroy()
    {
        GameModeManager.OnGameModeStateChange -= HandleGameModeStateChange;
        PracticeGameModeManager.UpdateDamageablesCountUI -= UpdateDamageablesCountUI;
        WeaponManager.OnGunChange -= UpdateGunUI;
        Gun.OnGunShoot -= UpdateGunUI;
    }

    private void DisableAllUI()
    {
        _practiceGameUI.gameObject.SetActive(false);
    }

    private void HandleGameModeStateChange(ModeType type, bool isStarted)
    {
        _currentGameMode = type;

        switch (type)
        {
            case ModeType.PRACTICE_MODE:
                _practiceGameUI.gameObject.SetActive(isStarted);
                break;
        }
    }

    private void UpdateDamageablesCountUI(int totalDamageables, int remainingDamageables)
    {
        switch (_currentGameMode)
        {
            case ModeType.PRACTICE_MODE:
                _practiceGameUI?.UpdateDamageablesUI(totalDamageables, remainingDamageables);
                break;
        }
        
    }

    private void UpdateGunUI(WeaponData data)
    {
        switch (_currentGameMode)
        {
            case ModeType.PRACTICE_MODE:
                _practiceGameUI?.UpdateWeaponsUI(data);
                break;
        }
    }
}
