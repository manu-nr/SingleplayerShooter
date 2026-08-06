using UnityEngine;
using UnityEngine.UI;

public class AimLabUIController : MonoBehaviour
{
    [SerializeField] private Canvas _aimLabCanvas;

    [SerializeField] private ModeButton _easyButton;
    [SerializeField] private ModeButton _mediumButton;
    [SerializeField] private ModeButton _hardButton;
    [SerializeField] private Button _startButton;

    private AimDifficulty _selectedDifficulty;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _easyButton.GetComponent<Button>().onClick.AddListener(() => SetGameMode(AimDifficulty.Easy));
        _mediumButton.GetComponent<Button>().onClick.AddListener(() => SetGameMode(AimDifficulty.Medium));
        _hardButton.GetComponent<Button>().onClick.AddListener(() => SetGameMode(AimDifficulty.Hard));

        ToggleStartButton(false);
    }

    private void SetGameMode(AimDifficulty difficulty)
    {
        _easyButton.SetTickMarkActive(difficulty == AimDifficulty.Easy);
        _mediumButton.SetTickMarkActive(difficulty == AimDifficulty.Medium);
        _hardButton.SetTickMarkActive(difficulty == AimDifficulty.Hard);

        ToggleStartButton(true);
    }

    private void ToggleStartButton(bool show)
    {
        if (_startButton != null)
            _startButton.interactable = show;
    }

    private void OnStartButtonClicked()
    {
        _aimLabCanvas.enabled = false;
        AimLabManager.Instance.StartGame(_selectedDifficulty);
    }

    private void ToggleCanvas(bool show)
    {
        if (_aimLabCanvas != null)
            _aimLabCanvas.enabled = show;
    }
}
