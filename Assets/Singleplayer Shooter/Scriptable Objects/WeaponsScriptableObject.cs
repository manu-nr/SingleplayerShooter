using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AllWeaponsData", fileName ="Create All Weapons Data")]
public class WeaponsScriptableObject : ScriptableObject
{
    [SerializeField] public List<BaseWeapon> _weaponPrefab;
}
