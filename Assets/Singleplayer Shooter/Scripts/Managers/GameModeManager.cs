using System;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private ModeType _gameMode;

    protected bool _isGameModeActive;
    protected ModeType _currentGameMode;

    public static event Action<ModeType, bool> OnGameModeStateChange;

    #region Unity Methods
    protected virtual void Start()
    {
        MainMenuManager.OnMenuOptionSelected += HandleMenuOptionSelected;
    }
    protected virtual void OnDestroy()
    {
        MainMenuManager.OnMenuOptionSelected -= HandleMenuOptionSelected;
    }
    #endregion

    #region Base Methods

    protected virtual void Begin()
    {
        _currentGameMode = ModeType.PRACTICE_MODE;
        _isGameModeActive = true;
        OnGameModeStateChange?.Invoke(_currentGameMode, _isGameModeActive);
    }

    protected virtual void Complete()
    {
        _isGameModeActive = false;
        OnGameModeStateChange?.Invoke(_currentGameMode, _isGameModeActive);
        _currentGameMode = ModeType.NONE;
    }
    #endregion

    #region Handle Methods
    private void HandleMenuOptionSelected(ModeType type, bool isSelected)
    {
        if (isSelected && type == _gameMode)
        {
            Begin();
        }
    }
    #endregion
}
public enum ModeType
{
    PRACTICE_MODE,
    NONE
}
