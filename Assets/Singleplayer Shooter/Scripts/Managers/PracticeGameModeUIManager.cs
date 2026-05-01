using TMPro;
using UnityEngine;

public class PracticeGameModeUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _remainingdamageablesCountText;
    [SerializeField] private TextMeshProUGUI _currentGun;
    [SerializeField] private TextMeshProUGUI _currentGunAmmoText;
    [SerializeField] private TextMeshProUGUI _totalAmmo;


    public void UpdateDamageablesUI(int totalDamageables, int remainingDamageables)
    {
        _remainingdamageablesCountText.SetText($"{remainingDamageables}/{totalDamageables}");
    }

    public void UpdateWeaponsUI(WeaponData data)
    {
        _currentGun.SetText("Gun: " + data.weaponName);
        _currentGunAmmoText.SetText($"Ammo: {WeaponManager.Instance.CurrentWeapon.CurrentAmmo} / {data.magazineSize}");
    }
}
