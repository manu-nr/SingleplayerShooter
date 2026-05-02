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
            ToggleMenu(ModeType.PRACTICE_MODE);
        }
    }

    #endregion

    private void HandleGameModeStateChange(ModeType type, bool isActive)
    {
        _menuCanvas.SetActive(!_isMenuActive);
    }

    private void ToggleMenu(ModeType type)
    {
        OnMenuOptionSelected?.Invoke(type, _isMenuActive);
        _isMenuActive = !_isMenuActive;
        _menuCanvas.SetActive(_isMenuActive);
    }
}
