using System;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;

    private bool _isMenuActive = true;

    public static event Action<ModeType, bool> OnMenuOptionSelected;

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
        if (Input.GetKeyDown(KeyCode.P) && _isMenuActive)
        {
            OnMenuOptionSelected?.Invoke(ModeType.PRACTICE_MODE, _isMenuActive);
        }
    }

    #endregion

    private void HandleGameModeStateChange(ModeType type, bool isActive)
    {
        ToggleMenu(type);
    }

    private void ToggleMenu(ModeType type)
    {
        _isMenuActive = !_isMenuActive;
        _menuCanvas.SetActive(_isMenuActive);
    }
}
