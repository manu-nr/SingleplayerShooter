using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DamagableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _damagable;
    [SerializeField] private int _count = 5;
    [SerializeField] private float xRange = 8f;
    [SerializeField] private float zRange = 6f;

    private List<GameObject> _damagableList = new List<GameObject>();
    [SerializeField] private int _currentIndex = -1;

    private void Start()
    {
        for(int i=0; i<_count; i++)
        {
            GameObject damagable = Instantiate(_damagable, transform);
            damagable.SetActive(false);
            _damagableList.Add(damagable);
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
            HideCurrentDamagable();

        _currentIndex++;

        if(_currentIndex == _count)
            _currentIndex = 0;

        _damagableList[_currentIndex].SetActive(true);
        _damagableList[_currentIndex].transform.SetLocalPositionAndRotation(GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xRange, xRange);
        float zPosition = Random.Range(-zRange, zRange);
        return new Vector3 (xPosition, transform.position.y, zPosition);
    }

    private void HideCurrentDamagable()
    {
        _damagableList[_currentIndex].SetActive(false);
    }
}
