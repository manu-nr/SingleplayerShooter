using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DamageableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _damageable;
    [SerializeField] private int _count = 5;
    [SerializeField] private float xRange = 8f;
    [SerializeField] private float zRange = 6f;

    private List<GameObject> _damageableList = new List<GameObject>();
    [SerializeField] private int _currentIndex = -1;

    private void Start()
    {
        for(int i=0; i<_count; i++)
        {
            GameObject damageable = Instantiate(_damageable, transform);
            damageable.SetActive(false);
            _damageableList.Add(damageable);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            EnableAndSetPosition();
    }

    private void EnableAndSetPosition()
    {
        if (_currentIndex >= 0)
            HideCurrentDamageable();

        _currentIndex++;

        if(_currentIndex == _count)
            _currentIndex = 0;

        _damageableList[_currentIndex].SetActive(true);
        _damageableList[_currentIndex].transform.SetLocalPositionAndRotation(GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xRange, xRange);
        float zPosition = Random.Range(-zRange, zRange);
        return new Vector3 (xPosition, transform.position.y, zPosition);
    }

    private void HideCurrentDamageable()
    {
        _damageableList[_currentIndex].SetActive(false);
    }
}
