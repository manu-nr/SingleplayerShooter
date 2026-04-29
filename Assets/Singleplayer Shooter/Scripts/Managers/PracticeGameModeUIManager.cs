using TMPro;
using UnityEngine;

public class PracticeGameModeUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _remainingdamageablesCountText;


    public void UpdateUI(WeaponData data, int totalDamageables, int remainingDamageables)
    {
        _remainingdamageablesCountText.SetText($"{remainingDamageables}/{totalDamageables}");
    }
}
