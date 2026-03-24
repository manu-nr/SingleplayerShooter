using UnityEngine;

public class Pistol : Gun
{
    public Camera cam;
    public float range = 100f;

    protected override void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(data.damage);
            }
        }

        Debug.Log("Pistol Shoot");
    }

    public override void Reload() { }
}