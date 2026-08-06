using UnityEngine;

public class ModeButton : MonoBehaviour
{
    [SerializeField] private GameObject _tickMark;

    private void Start()
    {
        SetTickMarkActive(false);
    }
    public void SetTickMarkActive(bool isActive)
    {
        if (_tickMark != null)
            _tickMark.SetActive(isActive);
    }
}
