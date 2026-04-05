using UnityEngine;

public class Pistol : Gun
{
    public GameObject _crossHair;
    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Use();
        }

        Debug.DrawRay(_crossHair.transform.position, _crossHair.transform.forward * range, Color.red);
    }

    protected override void Shoot()
    {
        Ray ray = new Ray(_crossHair.transform.position, _crossHair.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            //var damageable = hit.collider.GetComponent<IDamageable>();
            //if (damageable != null)
            //{
            //    damageable.TakeDamage(data.damage);
            //}
            //Debug.Log("[NRM] Entering to hit Damagable");

            if (hit.collider.CompareTag("Damagable"))
            {
                Debug.Log("[NRM] Hitted Damagable");
            }
        }

        Debug.Log("Pistol Shoot");
    }

    public override void Reload() { }
}