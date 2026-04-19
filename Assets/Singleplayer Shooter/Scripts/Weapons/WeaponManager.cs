using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private WeaponsScriptableObject _allWeaponsData;

    public List<BaseWeapon> weapons;
    int currentIndex = 0;

    BaseWeapon CurrentWeapon => weapons[currentIndex];

    public static WeaponManager Instance;

    public static Action<WeaponData> OnGunChange;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if(_weaponSpawner != null)
            weapons = _weaponSpawner.SpawnAllWeapons(_allWeaponsData);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentIndex == -1)
                return;

            CurrentWeapon.Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentIndex == -1)
                return;

            CurrentWeapon.Reload();
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
    }

    void SwitchWeapon(int index)
    {
        OnGunChange?.Invoke(weapons[index].data);
        weapons[currentIndex].gameObject.SetActive(false);
        currentIndex = index;
        weapons[currentIndex].gameObject.SetActive(true);
    }
}