using UnityEngine;

public interface IDamageable
{
    public float CurrentHealth { get; set; }
    public void OnHit(float damage, Vector2 knockback);
    public void OnHit(float damage);
}