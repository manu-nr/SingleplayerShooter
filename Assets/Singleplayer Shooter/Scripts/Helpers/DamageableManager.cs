using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DamageableManager : MonoBehaviour
{
    [SerializeField] private GameObject _damageable;
    [SerializeField] private int _count = 5;
    [SerializeField] private float xRange = 8f;
    [SerializeField] private float zRange = 6f;

    private List<Damageable> _damageableList = new List<Damageable>();
    private int _currentIndex = -1;
    private Damageable _currentDamageable;

    #region Unity Methods

    private void Start()
    {
        for(int i=0; i<_count; i++)
        {
            GameObject damageableGameObject = Instantiate(_damageable, transform);
            damageableGameObject.SetActive(false);
            Damageable damageable = damageableGameObject.GetComponent<Damageable>();
            _damageableList.Add(damageable);
        }

        Gun.OnGunShoot += HandleOnGunShoot;
    }

    private void OnDestroy()
    {
        Gun.OnGunShoot -= HandleOnGunShoot;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            EnableAndSetPosition();
    }

    #endregion

    #region Private Methods
    private void EnableAndSetPosition()
    {
        if (_currentIndex >= 0)
            HideCurrentDamageable();

        _currentIndex++;

        if(_currentIndex == _count)
            _currentIndex = 0;

        _currentDamageable = _damageableList[_currentIndex];

        if (_currentDamageable == null)
            return;

        _currentDamageable.gameObject.SetActive(true);
        _currentDamageable._health = 100f; 
        _currentDamageable.transform.SetLocalPositionAndRotation(GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xRange, xRange);
        float zPosition = Random.Range(-zRange, zRange);
        return new Vector3 (xPosition, transform.position.y, zPosition);
    }

    private void HideCurrentDamageable()
    {
        _currentDamageable?.gameObject.SetActive(false);
    }
    #endregion

    #region Handlers
    private void HandleOnGunShoot(WeaponData data)
    {
        if(data != null && _currentDamageable != null)
        {
            _currentDamageable.TakeDamage(data.damage);

            if(_currentDamageable._health <= 0)
                EnableAndSetPosition();
        }
    }
    #endregion
}
