using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;

    public List<Gun> SpawnAllWeapons(WeaponsScriptableObject allWeapons)
    {
        List<Gun> spawnedWeapons = new List<Gun>();

        foreach(var weapon in allWeapons._weaponPrefab)
        {
            Gun baseWeapon = Instantiate(weapon, _weaponHolder);
            baseWeapon.gameObject.SetActive(false);
            spawnedWeapons.Add(baseWeapon);
        }

        return spawnedWeapons;
    }


}
