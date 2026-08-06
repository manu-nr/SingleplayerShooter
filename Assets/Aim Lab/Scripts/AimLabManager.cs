using UnityEngine;

public class AimLabManager : MonoBehaviour
{
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private PlayerController _playerController;

    public static AimLabManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        TogglePlayer(false);
    }

    private void TogglePlayer(bool show)
    {
        if(_playerController != null)
        {
            _playerController.gameObject.SetActive(show);
        }
    }

    public void StartGame(AimDifficulty difficulty)
    {
        TogglePlayer(true);
    }


}

public enum AimDifficulty
{
    None,
    Easy,
    Medium,
    Hard
}
