using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] public float _health = 100f;


    public void Destroy()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
