using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private WeaponsScriptableObject _allWeaponsData;

    public List<Gun> weapons;
    private int _currentIndex = -1;

    public Gun CurrentWeapon => weapons[_currentIndex];

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
            if (_currentIndex == -1)
                return;

            CurrentWeapon.Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_currentIndex == -1)
                return;

            CurrentWeapon.Reload();
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
    }

    public void SwitchWeapon(int index)
    {
        if(_currentIndex != -1)
            weapons[_currentIndex].gameObject.SetActive(false);

        _currentIndex = index;
        weapons[_currentIndex].gameObject.SetActive(true);
        OnGunChange?.Invoke(weapons[index].data);
    }

    public void HideCurrentGun()
    {
        if(_currentIndex != -1)
        {
            weapons[_currentIndex].gameObject.SetActive(false);
            _currentIndex = -1;
        }
    }
}