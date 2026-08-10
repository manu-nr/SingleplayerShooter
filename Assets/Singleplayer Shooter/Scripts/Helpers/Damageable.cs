using Unity.VisualScripting;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] public float _health = 100f;

    [SerializeField] private float _size = 1f;
    [SerializeField] private float _speed = 1f;

    [SerializeField] private bool _canMove = true;


    private void Update()
    {
        if(_canMove)
        {

        }
    }

    public void Destroy()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void SetDamageableSize(float size)
    {
        _size = size;
    }

    public void SetDamageableSpeed(float speed)
    {
        _speed = speed;
    }
}
