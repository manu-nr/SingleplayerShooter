using UnityEngine;

namespace AimLab
{
    public class AimLabGameModeManager : MonoBehaviour
    {
        [Header("Mode Scriptable Objects")]
        [SerializeField] private GameModeScriptableObject _easyMode;
        [SerializeField] private GameModeScriptableObject _mediumMode;
        [SerializeField] private GameModeScriptableObject _hardMode;
        
        [Space]
        [Header("Current Data")]
        [SerializeField] private GameModeScriptableObject _currentMode;
        [SerializeField] private float _currentDamageableSpeed;
        [SerializeField] private float _currentDamageableSize;

        private void Start()
        {
            AimLabManager.OnGameModeSelected += OnGameModeSelected;
        }

        private void OnDestroy()
        {
            AimLabManager.OnGameModeSelected -= OnGameModeSelected;
        }

        private void OnGameModeSelected(bool started, AimDifficulty difficulty)
        {
            //_currentMode = GetCurrentModeData(difficulty);
            //if (_currentMode != null)
            //{
            //    _currentDamageableSpeed = _currentMode.damageableSpeed;
            //    _currentDamageableSize = _currentMode.damageableSize;
            //}
        }

        private GameModeScriptableObject GetCurrentModeData(AimDifficulty difficulty)
        {
            return difficulty switch
            {
                AimDifficulty.Easy => _easyMode,
                AimDifficulty.Medium => _mediumMode,
                AimDifficulty.Hard => _hardMode,
                _ => null
            };
        }
    }
}
