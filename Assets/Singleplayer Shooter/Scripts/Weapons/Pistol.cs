using UnityEngine;

public class Pistol : Gun
{
    public GameObject _crossHair;
    public float range = 100f;

    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        Use();
    //    }

    //    Debug.DrawRay(_crossHair.transform.position, _crossHair.transform.forward * range, Color.red);
    //}

    protected override void Shoot()
    {
        
    }

    public override void Reload() { }
}