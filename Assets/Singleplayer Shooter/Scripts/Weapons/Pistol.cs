using UnityEngine;

public class Pistol : Gun
{
    public float range = 100f;

    protected override void Shoot()
    {
        Debug.Log("[NRM] On shoot");
    }

}