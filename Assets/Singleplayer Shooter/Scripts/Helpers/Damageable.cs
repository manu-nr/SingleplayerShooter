using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;

    public void TakeDamage(float damage)
    {
        
    }
}
