using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<BaseWeapon> weapons;
    int currentIndex = 0;

    BaseWeapon CurrentWeapon => weapons[currentIndex];

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButton("Fire1"))
        {
            CurrentWeapon.Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentWeapon.Reload();
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
    }

    void SwitchWeapon(int index)
    {
        weapons[currentIndex].gameObject.SetActive(false);
        currentIndex = index;
        weapons[currentIndex].gameObject.SetActive(true);
    }
}