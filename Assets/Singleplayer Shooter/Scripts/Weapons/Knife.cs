using UnityEngine;

public class Knife : BaseWeapon
{
    public float range = 2f;

    public override void Use()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(data.damage);
            }
        }

        Debug.Log("Knife Attack");
    }

    public override void Reload() { }
}