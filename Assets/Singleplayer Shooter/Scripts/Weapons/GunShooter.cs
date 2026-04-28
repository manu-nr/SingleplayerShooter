using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _range = 100f;

    private void Start()
    {
        Gun.OnGunShoot += HandleGunShoot;
    }

    private void OnDestroy()
    {
        Gun.OnGunShoot -= HandleGunShoot;
    }

    private void HandleGunShoot(WeaponData data)
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range))
        {
            if (hit.collider.CompareTag("Damagable"))
            {
                Debug.Log("[NRM] Hitted Damagable");
            }
        }
    }
}
