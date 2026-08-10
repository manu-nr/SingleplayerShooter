using System;
using UnityEngine;

namespace AimLab
{
    public class AimLabManager : MonoBehaviour
    {
        [SerializeField] private WeaponManager _weaponManager;
        [SerializeField] private PlayerController _playerController;

        public static AimLabManager Instance;
        public static Action<bool, AimDifficulty> OnGameModeSelected;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {

        }


        public void StartGame(AimDifficulty difficulty)
        {
            OnGameModeSelected?.Invoke(true, difficulty);
        }

        public void EndGame(AimDifficulty difficulty)
        {
            OnGameModeSelected?.Invoke(false, difficulty);
        }


    }
}

public enum AimDifficulty
{
    None,
    Easy,
    Medium,
    Hard
}