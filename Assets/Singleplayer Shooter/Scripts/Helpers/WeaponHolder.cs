using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.rotation = _cameraTransform.rotation;
    }
}
