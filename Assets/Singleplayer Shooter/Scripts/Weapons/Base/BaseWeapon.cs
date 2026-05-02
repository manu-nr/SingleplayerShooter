using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;

    public abstract void Use();     // shoot or attack
    public abstract void Reload();  // optional
}
