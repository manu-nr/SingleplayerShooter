using System;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PracticeGameModeUIManager _practiceGameUI;

    private ModeType _currentGameMode;


    private void Start()
    {
        GameModeManager.OnGameModeStateChange += HandleGameModeStateChange;
        PracticeGameModeManager.UpdateUI += UpdatePracticeModeUI;

        DisableAllUI();
    }

    private void OnDestroy()
    {
        GameModeManager.OnGameModeStateChange -= HandleGameModeStateChange;
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

    private void UpdatePracticeModeUI(WeaponData data, int totalDamageables, int remainingDamageables)
    {
        _practiceGameUI?.UpdateUI(data, totalDamageables, remainingDamageables);
    }
}
