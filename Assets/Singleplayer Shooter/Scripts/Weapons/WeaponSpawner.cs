using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;

    public List<BaseWeapon> SpawnAllWeapons(WeaponsScriptableObject allWeapons)
    {
        List<BaseWeapon> spawnedWeapons = new List<BaseWeapon>();

        foreach(var weapon in allWeapons._weaponPrefab)
        {
            BaseWeapon baseWeapon = Instantiate(weapon, _weaponHolder);
            baseWeapon.gameObject.SetActive(false);
            spawnedWeapons.Add(baseWeapon);
        }

        return spawnedWeapons;
    }


}
